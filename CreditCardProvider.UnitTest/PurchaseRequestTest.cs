using CreditCardProvider.Data;
using CreditCardProvider.Models.CCPurchase;
using Moq;

namespace CreditCardProvider.UnitTest
{
    public class PurchaseRequestTest
    {
        //PurchaseRequest purchaseRequestClass = new PurchaseRequest();
        private PurchaseRequest _purchaseRequest { get; set; } = null!;
        Mock<CCDBContext> mockCCDBContect = new Mock<CCDBContext>();
        public PurchaseRequestTest() {  }


        [SetUp]
        public void Setup()
        {
            _purchaseRequest = new PurchaseRequest(mockCCDBContect);
        }

        [TestCase("111222", "Smith", 500)] //Card limit is $1000
        [TestCase("111222", "Smith", 200)]
        [TestCase("333444", "Tao", 1000)] //Card limit is $1000
        [TestCase("555666", "Mayhew", 600)] //card limit is $500 string cardNumber, string lastName, int purchaseAmount       
        public void AddPurchaseTranscationTest(string cardNumber, string lastName, int purchaseAmount)
        {
            //Assign
            //var cardNumber = "111222";
            //var lastName = "Smith";
            //var purchaseAmount = 500;

            //Act
            var resultText = _purchaseRequest.AddPurchaseTranscation(cardNumber, lastName, purchaseAmount);


            //Assert
            //Assert.AreEqual("Purchase sucessful.", resultText);
            Assert.That(resultText, Is.EqualTo("Purchase sucessful."));
        }

        [TestCase("111222", "Smith")]
        public void ReturnMinimumRepaymentTest1(string cardNumber, string lastName)
        {
            //Assign
            //var cardNumber = "111222";
            //var lastName = "Smith";

            //Act
            var returnMinPayment = _purchaseRequest.ReturnMinimumPayment(cardNumber, lastName);

            //Assert
            Assert.That(returnMinPayment, Is.EqualTo(70));
        }

        [TestCase("333444", "Tao")]
        public void ReturnMinimumRepaymentTest2(string cardNumber, string lastName)
        {
            //Assign
            //var cardNumber = "111222";
            //var lastName = "Smith";

            //Act
            var returnMinPayment = _purchaseRequest.ReturnMinimumPayment(cardNumber, lastName);

            //Assert
            Assert.That(returnMinPayment, Is.EqualTo(100));
        }


    }
}