using Mather.Data.States.StateBranch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MatherTest
{
    internal class AddTest
    {
        public FlowDocument document;
        public StateBranch branch;
        public State state;
        [TestInitialize]
        public void Init()
        {
            document = new FlowDocument(new Paragraph(new Run("Новый документ")));
            branch = new("Ветвь");
            state = new State("Статья", document);
        }
        [TestMethod]
        public void AddOneTest()
        {
            branch.Add(state);
            Assert.AreEqual(branch.GetItem(0), state);
        }
        [TestMethod]
        public void AddTwoTest()
        {
            State newState = new State("Статья 2", new FlowDocument(new Paragraph(new Run("Новый документ 2"))));
            branch.Add(state);
            branch.Add(newState);
            Assert.AreEqual(branch.GetItem(0), state);
            Assert.AreEqual(branch.GetItem(1), newState);
        }

        [TestMethod]
        public void AddInnerTest()
        {
            StateBranch newBranch = new StateBranch("Ветвь 2");
            newBranch.Add(state);
            branch.Add(newBranch);
            Assert.AreEqual(branch.GetItem(0).GetItem(0), state);
        }
    }
}
