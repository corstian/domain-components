﻿using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using Domain.Example.Aggregates.UserAggregate.Snapshots;
using System;
using Xunit;

namespace Domain.Example.Tests
{
    public class UserTests
    {
        [Fact]
        public void UserNameShouldBeSet()
        {
            var user = new User();
            var command = new Rename
            {
                Name = "John Doe"
            };

            var result = user.Evaluate(command);

            user.Apply(result);

            Assert.Equal("John Doe", user.Name);
        }

        [Fact]
        public void EmailShouldBeSet()
        {
            var user = new User();
            var command = new ChangeEmail
            {
                Email = "john.doe@example.com"
            };

            var result = user.Evaluate(command);

            user.Apply(result);

            Assert.Equal("john.doe@example.com", user.Email);
        }

        [Fact]
        public void EmailMustContainAt()
        {
            var user = new User();
            var command = new ChangeEmail();

            var result = user.Evaluate(command);

            Assert.True(result.IsFailed);
            Assert.False(result.IsSuccess);
            Assert.Equal("No @", result.Errors[0].Message);
            Assert.Throws<InvalidOperationException>(() => result.Value);
            Assert.Null(result.ValueOrDefault);
        }

        [Fact]
        public void CanValidatePassword()
        {
            var user = new User();

            var passwordChanged = user
                .Evaluate(new ChangePassword
                {
                    Password = "1234"
                }).Value;

            user.Apply(passwordChanged);

            var passwordCorrectlyValidated = user
                .Evaluate(new ValidatePassword
                {
                    Password = "1234"
                }).Value;

            Assert.True(passwordCorrectlyValidated.Succeeded);

            user.Apply(passwordCorrectlyValidated);

            var passwordIncorrectlyValidated = user
                .Evaluate(new ValidatePassword
                {
                    Password = "123"
                }).Value;

            Assert.False(passwordIncorrectlyValidated.Succeeded);

            user.Apply(passwordIncorrectlyValidated);

            Assert.Equal(2, user.LoginAttempts.Count);
        }

        [Fact]
        public void PasswordCannotBeSameAsPrevious()
        {
            var user = new User();
            var command = new ChangePassword { Password = "1234" };
            
            var pw1 = user.Evaluate(command).Value;
            user.Apply(pw1);

            var pw2 = user.Evaluate(command);

            Assert.True(pw2.IsFailed);
            Assert.Equal("Password cannot be the same as a previous password", pw2.Errors[0].Message);
        }

        [Fact]
        public void InfoCanBeChanged()
        {
            var user = new User();

            var command = new ChangeInfo
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            var (renamed, emailChanged) = user.Evaluate(command).Value;

            user.Apply(renamed, emailChanged);

            Assert.Equal("John Doe", user.Name);
            Assert.Equal("john.doe@example.com", user.Email);
        }

        [Fact]
        public void SnapshotShouldBeReturned()
        {
            var user = new User();
            var command = new ChangeInfo
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            var (renamed, emailChanged) = user.Evaluate(command).Value;
            
            var snapshot = user.Apply<PublicUserInfo>(renamed, emailChanged);

            Assert.Equal("john.doe@example.com", snapshot.Email);
            Assert.Equal("John Doe", snapshot.Name);
        }
    }
}
