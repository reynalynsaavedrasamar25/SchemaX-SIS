using System;
using SchemaX.SIS.Helpers;
using Xunit;

namespace SchemaX.ValidatorTests
{
    public class UnitTest1
    {
        [Fact]
        public void Birthday_PastDate_ShouldBeValid()
        {
            // Arrange: Prepare a birthday in the past.
            DateTime birthday = new DateTime(2005, 5, 15);

            // Act: Call the actual validator.
            bool result = Validator.IsBirthdayValid(birthday);

            // Assert: A past birthday should be accepted.
            Assert.True(result);
        }
        [Fact]
        public void Birthday_FutureDate_ShouldBeInvalid()
        {
            DateTime birthday = DateTime.Today.AddDays(1);

            bool result = Validator.IsBirthdayValid(birthday);

            Assert.False(result);
        }
        [Fact]
        public void RequiredField_Empty_ShouldBeInvalid()
        {
            string value = "";

            bool result = Validator.IsRequiredFieldValid(value);

            Assert.False(result);
        }
        [Fact]
        public void RequiredField_Whitespace_ShouldBeInvalid()
        {
            string value = "   ";

            bool result = Validator.IsRequiredFieldValid(value);

            Assert.False(result);
        }
        [Fact]
        public void RequiredField_ValidText_ShouldBeValid()
        {
            string value = "John";

            bool result = Validator.IsRequiredFieldValid(value);

            Assert.True(result);
        }
        [Fact]
        public void Name_Valid_ShouldBeAccepted()
        {
            string name = "John Manuel";

            bool result = Validator.IsNameValid(name);

            Assert.True(result);
        }
        [Fact]
        public void Name_WithNumbers_ShouldBeInvalid()
        {
            string name = "John123";

            bool result = Validator.IsNameValid(name);

            Assert.False(result);
        }
        [Fact]
        public void Name_WithHyphen_ShouldBeValid()
        {
            string name = "Anne-Marie";

            bool result = Validator.IsNameValid(name);

            Assert.True(result);
        }
        [Fact]
        public void Name_WithApostrophe_ShouldBeValid()
        {
            string name = "O'Connor";

            bool result = Validator.IsNameValid(name);

            Assert.True(result);
        }
        [Fact]
        public void BuEmail_ValidAddress_ShouldBeAccepted()
        {
            string email = "student@bu.edu.ph";

            bool result = Validator.IsBuEmailValid(email);

            Assert.True(result);
        }
        [Fact]
        public void BuEmail_GmailAddress_ShouldBeInvalid()
        {
            string email = "student@gmail.com";

            bool result = Validator.IsBuEmailValid(email);

            Assert.False(result);
        }
        [Fact]
        public void ContactNumber_Valid11Digits_ShouldBeAccepted()
        {
            string contactNumber = "09123456789";

            bool result = Validator.IsContactNumberValid(contactNumber);

            Assert.True(result);
        }
        [Fact]
        public void ContactNumber_TooShort_ShouldBeInvalid()
        {
            string contactNumber = "123456";

            bool result = Validator.IsContactNumberValid(contactNumber);

            Assert.False(result);
        }
        [Fact]
        public void ContactNumber_TooLong_ShouldBeInvalid()
        {
            string contactNumber = "1234567890123456";

            bool result = Validator.IsContactNumberValid(contactNumber);

            Assert.False(result);
        }
        [Fact]
        public void ContactNumber_WithLetters_ShouldBeInvalid()
        {
            string contactNumber = "09123ABC789";

            bool result = Validator.IsContactNumberValid(contactNumber);

            Assert.False(result);
        }
        [Fact]
        public void StudentId_InvalidFormat_ShouldBeRejected()
        {
            string studentId = "202600001";

            bool result = Validator.IsStudentIdValid(studentId);

            Assert.False(result);
        }
        [Fact]
        public void StudentId_ValidFormat_ShouldBeAccepted()
        {
            string studentId = "2026-00001";

            bool result = Validator.IsStudentIdValid(studentId);

            Assert.True(result);
        }
        [Fact]
        public void StudentId_Empty_ShouldBeRejected()
        {
            string studentId = "";

            bool result = Validator.IsStudentIdValid(studentId);

            Assert.False(result);
        }
        [Fact]
        public void StudentId_WithLetters_ShouldBeRejected()
        {
            string studentId = "2026-ABCDE";

            bool result = Validator.IsStudentIdValid(studentId);

            Assert.False(result);
        }
        [Fact]
        public void StudentId_TooShort_ShouldBeRejected()
        {
            string studentId = "2026-0001";

            bool result = Validator.IsStudentIdValid(studentId);

            Assert.False(result);
        }
        [Fact]
        public void StudentId_TooLong_ShouldBeRejected()
        {
            string studentId = "2026-000001";

            bool result = Validator.IsStudentIdValid(studentId);

            Assert.False(result);
        }
        [Fact]
        public void StudentId_Whitespace_ShouldBeRejected()
        {
            string studentId = "   ";

            bool result = Validator.IsStudentIdValid(studentId);

            Assert.False(result);
        }
        [Fact]
        public void Name_WithSpecialCharacters_ShouldBeInvalid()
        {
            string name = "John@Doe";

            bool result = Validator.IsNameValid(name);

            Assert.False(result);
        }
        [Fact]
        public void BuEmail_MissingAtSymbol_ShouldBeInvalid()
        {
            string email = "studentbu.edu.ph";

            bool result = Validator.IsBuEmailValid(email);

            Assert.False(result);
        }
        [Fact]
        public void BuEmail_WrongDomain_ShouldBeInvalid()
        {
            string email = "student@gmail.com";

            bool result = Validator.IsBuEmailValid(email);

            Assert.False(result);
        }
    }
}
