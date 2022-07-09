using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using Domain.Example.Aggregates.UserAggregate.Snapshots;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Example.Tests
{
    public class UserTests
    {
        [Fact]
        public async Task UserNameShouldBeSet()
        {
            var user = new User();
            var command = new Rename
            {
                Name = "John Doe"
            };

            var result = await user.Evaluate(command);

            await user.Apply((IEvent<User>)result.Value);

            Assert.Equal("John Doe", user.Name);
        }

        [Fact]
        public async Task EmailShouldBeSet()
        {
            var user = new User();
            var command = new ChangeEmail
            {
                Email = "john.doe@example.com"
            };

            var result = await user.Evaluate(command);

            await user.Apply((IEvent<User>)result.Value);

            Assert.Equal("john.doe@example.com", user.Email);
        }

        [Fact]
        public async Task EmailMayNotBeEmpty()
        {
            var user = new User();
            var command = new ChangeEmail();

            var result = await user.Evaluate(command);

            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Reasons);
        }

        [Fact]
        public async Task EmailMustContainAt()
        {
            var user = new User();
            var command = new ChangeEmail();

            var result = await user.Evaluate(command);

            Assert.True(result.IsFailed);
            Assert.False(result.IsSuccess);
            Assert.Equal("No @", result.Errors[0].Message);
            Assert.Throws<InvalidOperationException>(() => result.Value);
            Assert.Null(result.ValueOrDefault);
        }

        [Fact]
        public async Task CanValidatePassword()
        {
            var user = new User();

            var passwordChanged = (await user
                .Evaluate(new ChangePassword
                {
                    Password = "1234"
                })).Value;

            await user.Apply((IEvent<User>)passwordChanged);

            var passwordCorrectlyValidated = (await user
                .Evaluate(new ValidatePassword
                {
                    Password = "1234"
                })).Value;

            Assert.True(passwordCorrectlyValidated.Succeeded);

            await user.Apply((IEvent<User>)passwordCorrectlyValidated);

            var passwordIncorrectlyValidated = (await user
                .Evaluate(new ValidatePassword
                {
                    Password = "123"
                })).Value;

            Assert.False(passwordIncorrectlyValidated.Succeeded);

            await user.Apply((IEvent<User>)passwordIncorrectlyValidated);

            Assert.Equal(2, user.LoginAttempts.Count);
        }

        [Fact]
        public async Task PasswordCannotBeSameAsPrevious()
        {
            var user = new User();
            var command = new ChangePassword { Password = "1234" };
            
            var pw1 = (await user.Evaluate(command)).Value;
            await user.Apply((IEvent<User>)pw1);

            var pw2 = await user.Evaluate(command);

            Assert.True(pw2.IsFailed);
            Assert.Equal("Password cannot be the same as a previous password", pw2.Errors[0].Message);
        }

        [Fact]
        public async Task InfoCanBeChanged()
        {
            var user = new User();

            var command = new ChangeInfo
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            var result = await user.Evaluate(command);

            await user.Apply(result.Value);

            Assert.Equal("John Doe", user.Name);
            Assert.Equal("john.doe@example.com", user.Email);
        }

        [Fact]
        public async Task SnapshotShouldReflectState()
        {
            var user = new User();
            var command = new ChangeInfo
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            var result = await user.Evaluate(command);
            
            await user.Apply(result.Value);

            var snapshot = await user.GetSnapshot<PublicUserInfo>();

            Assert.Equal("john.doe@example.com", snapshot.Email);
            Assert.Equal("John Doe", snapshot.Name);
        }
    }
}
