using Mather.Data.Tasks.Equations.Operations;
using Mather.Windows;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mather.Data.Tasks.Equations
{
    /// <summary>
    /// Логика взаимодействия для EquationControl.xaml
    /// </summary>
    public partial class EquationControl : UserControl
    {
        const double defaultFontSize = 32;
        private Equation equation;
        private int currentFontSize;
        public EquationControl()
        {
            InitializeComponent();

            // Пример уравнения: 6x / (3 + 2) = x^2
            equation = new Equation(
                new Division(
                    new Multiplication(new Constant(5), new Variable("x")),
                    new Brackets(new Addition(new Constant(3), new Constant(2)))
                ),
                new Pow(new Variable("x"), new Constant(2))
            );
            DisplayEquation();
        }

        private void DisplayEquation()
        {
            EquationPanel.Children.Clear();

            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            AddEquationElementToPanel(equation.Left, stackPanel);
            stackPanel.Children.Add(new TextBlock { Text = " = ", FontSize = defaultFontSize, VerticalAlignment = VerticalAlignment.Center });
            AddEquationElementToPanel(equation.Right, stackPanel);

            EquationPanel.Children.Add(stackPanel);
        }

        private void AddEquationElementToPanel(EquationElement element, Panel parentPanel, double fontSize = defaultFontSize, int level = 0)
        {
            if (element is Constant constant)
            {
                var button = CreateEquationButton(new TextBlock { Text = constant.GetText(), FontSize = fontSize }, element);
                parentPanel.Children.Add(button);
            }
            else if (element is Variable variable)
            {
                var button = CreateEquationButton(new TextBlock { Text = variable.GetText(), FontSize = fontSize }, element);
                parentPanel.Children.Add(button);
            }
            else if (element is Division division)
            {
                var stackPanel = new StackPanel { Orientation = Orientation.Vertical };
                AddEquationElementToPanel(division.Left, stackPanel, fontSize, level);
                var button = CreateEquationButton(new Separator { Height = 2, Background = Brushes.Black }, element);
                button.BorderThickness = new Thickness(0, 0, 0, 2);
                button.BorderBrush = Brushes.Black;
                stackPanel.Children.Add(button);
                AddEquationElementToPanel(division.Right, stackPanel, fontSize, level);
                parentPanel.Children.Add(stackPanel);
            }
            else if (element is Pow pow)
            {
                var basePanel = new StackPanel { Orientation = Orientation.Horizontal };
                AddEquationElementToPanel(pow.Left, basePanel, fontSize, level);

                var exponentPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(2, 0, 0, 0) };
                AddEquationElementToPanel(pow.Right, exponentPanel, fontSize * 0.75, level + 1);

                var superscriptPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Top
                };
                superscriptPanel.Children.Add(exponentPanel);
                basePanel.Children.Add(superscriptPanel);

                var button = CreateEquationButton(basePanel, element);
                parentPanel.Children.Add(button);
            }
            else if (element is Root root)
            {
                var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                var rootSymbol = new TextBlock
                {
                    Text = "√",
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = fontSize,
                    Margin = new Thickness(0, 0, 2, 0)
                };
                stackPanel.Children.Add(rootSymbol);
                AddEquationElementToPanel(root.Right, stackPanel, fontSize, level);
                parentPanel.Children.Add(stackPanel);
            }
            else if (element is Brackets brackets)
            {
                var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                var leftBracket = new TextBlock
                {
                    Text = "(",
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = fontSize,
                    Margin = new Thickness(0, 0, 2, 0)
                };
                var rightBracket = new TextBlock
                {
                    Text = ")",
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = fontSize,
                    Margin = new Thickness(0, 0, 2, 0)
                };

                stackPanel.Children.Add(leftBracket);
                AddEquationElementToPanel(brackets.Value, stackPanel, fontSize, level);
                stackPanel.Children.Add(rightBracket);
                parentPanel.Children.Add(stackPanel);
            }
            else if (element is Operation operation)
            {
                var stackPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center };
                AddEquationElementToPanel(operation.Left, stackPanel, fontSize, level);
                var operatorButton = CreateEquationButton(GetOperatorSymbol(operation), operation, fontSize);
                stackPanel.Children.Add(operatorButton);
                AddEquationElementToPanel(operation.Right, stackPanel, fontSize, level);
                parentPanel.Children.Add(stackPanel);
            }
        }

        private Button CreateEquationButton(string content, EquationElement element, double fontSize)
        {
            var button = new Button
            {
                Content = new TextBlock
                {
                    Text = content,
                    FontSize = fontSize
                },
                Style = (Style)Resources["EquationButtonStyle"]
            };
            button.Click += (s, e) => OnElementClick(element);
            button.Tag = element;
            return button;
        }

        private Button CreateEquationButton(string content, EquationElement element)
        {
            var button = new Button
            {
                Content = content,
                Style = (Style)Resources["EquationButtonStyle"],
                ContextMenu = (ContextMenu)Resources["OperationContextMenu"],
                Tag = element
            };
            button.Click += (s, e) => { OnElementClick(element); e.Handled = true; };
            return button;
        }
        private Button CreateEquationButton(UIElement content, EquationElement element)
        {
            var button = new Button
            {
                Content = content,
                Style = (Style)Resources["EquationButtonStyle"],
                ContextMenu = (ContextMenu)Resources["OperationContextMenu"],
                Tag = element
            };
            button.Click += (s, e) => OnElementClick(element);
            return button;
        }

        private string GetOperatorSymbol(Operation operation)
        {
            return operation switch
            {
                Addition => "+",
                Subtraction => "-",
                Multiplication => "×",
                Division => "/",
                Pow => "^",
                Root => "√",
                _ => "?"
            };
        }

        private void OnElementClick(EquationElement element)
        {
            ReplaceElementInCurrentEquation(element, element.Calculate());
            DisplayEquation();
        }

        private void ReplaceElementInCurrentEquation(EquationElement oldElement, EquationElement newElement)
        {
            equation.Left = ReplaceElementRecursive(equation.Left, oldElement, newElement);
            equation.Right = ReplaceElementRecursive(equation.Right, oldElement, newElement);
        }

        private EquationElement ReplaceElementRecursive(EquationElement current, EquationElement oldElement, EquationElement newElement)
        {
            if (current == oldElement)
                return newElement;

            if (current is Operation operation)
            {
                operation.Left = ReplaceElementRecursive(operation.Left, oldElement, newElement);
                operation.Right = ReplaceElementRecursive(operation.Right, oldElement, newElement);
            }
            else if (current is Brackets brackets)
            {
                brackets.Value = ReplaceElementRecursive(brackets.Value, oldElement, newElement);
            }

            return current;
        }

        private EquationElement FindParent(EquationElement root, EquationElement target)
        {
            if (root is Operation operation)
            {
                if (operation.Left == target || operation.Right == target)
                {
                    return root;
                }

                var leftSearch = FindParent(operation.Left, target);
                if (leftSearch != null)
                {
                    return leftSearch;
                }

                var rightSearch = FindParent(operation.Right, target);
                if (rightSearch != null)
                {
                    return rightSearch;
                }
            }
            else if (root is Brackets brackets)
            {
                if (brackets.Value == target)
                {
                    return root;
                }

                return FindParent(brackets.Value, target);
            }

            return null;
        }

        private void AddOperationContext_Click(object sender, RoutedEventArgs e)
        {
            var target = GetTarget(sender as MenuItem);
            var parent = FindParent(equation, target);
            CreateEquationWindow window = new CreateEquationWindow(target);
            window.ShowDialog();
            if(window.DialogResult == true)
            {
                if (parent is Brackets brackets)
                {
                    brackets.Value = window.Element;
                }
                else if(parent is Operation operation)
                {
                    if(operation.Left == target) operation.Left = window.Element;
                    else if(operation.Right == target) operation.Right = window.Element;
                }
            }
            DisplayEquation();
        }

        private EquationElement GetTarget(MenuItem sender)
        {
            var menu = (ContextMenu)sender.Parent;
            var button = menu.PlacementTarget as Button;
            return button.Tag as EquationElement;
        }

        private void RemoveOperationContext_Click(object sender, RoutedEventArgs e)
        {
            var target = GetTarget(sender as MenuItem);
            var parent = FindParent(equation, target) as Operation;
            if (parent.Left == target) ReplaceElementInCurrentEquation(parent, parent.Right);
            else if (parent.Right == target) ReplaceElementInCurrentEquation(parent, parent.Left);
            DisplayEquation();
        }
    }
}
