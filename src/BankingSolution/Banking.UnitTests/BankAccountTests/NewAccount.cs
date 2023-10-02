
using Banking.Domain;

namespace Banking.UnitTests.BankAccountTests;
public class NewAccount
{
    [Fact]
    public void NewAccountHasCorrectOpeningBalance()
    {
        // Given I have a new account
        var account = new Account();

        // When I ask that account for the balance
        decimal balance = account.GetBalance();

        // Then I am given the appropriate balance.
        Assert.Equal(5000M, balance);
    }
}
