using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        private Stack<string> stack = new Stack<string>();
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Push_ArgsIsNull_ThrowsArgumentException()
        {
            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ArgIsNotNull_AddOneItem()
        {
            var stackCountBeforeAdd = stack.Count;

            stack.Push("new string");

            var stackCountAfterAdd = stack.Count;

            
            Assert.That(stackCountAfterAdd.Equals(stackCountBeforeAdd+1));
        }

        [Test]
        public void Pop_StackIsEmpty_ThrowsInvalidOperationException()
        {
            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackHasSomething_ReturnLastItemAndRemovesIt()
        {
            var itemPushed = "new string";
            stack.Push(itemPushed);
            
            var stackCountAfterAdd = stack.Count;

            var itemPopped = stack.Pop();

            var stackCountAfterPop = stack.Count;
            
            Assert.That(stackCountAfterAdd.Equals(stackCountAfterPop+1));
            Assert.That(itemPushed.Equals(itemPopped));
        }

        [Test]
        public void Peek_StackIsEmpty_ThrowsInvalidOperationException()
        {
            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackHasSomething_ReturnLastItem()
        {
            var itemPushed = "new string";
            stack.Push(itemPushed);
            
            var stackCountAfterAdd = stack.Count;

            var itemPopped = stack.Peek();

            var stackCountAfterPop = stack.Count;
            
            Assert.That(stackCountAfterAdd.Equals(stackCountAfterPop));
            Assert.That(itemPushed.Equals(itemPopped));
        }
    }
}
