namespace Banking.Domain;

public class Account
{
    private readonly ICalculateBonuses _bonusCalculator;

    public Account(ICalculateBonuses bonusCalculator)
    {
        _bonusCalculator = bonusCalculator;
    }

    private decimal _balance = 5000M;
    public void Deposit(decimal amountToDeposit)
    {
        decimal bonus = _bonusCalculator.GetBonusForDepositOn(_balance, amountToDeposit);

        _balance += amountToDeposit + bonus;
    }

    public decimal GetBalance()
    {
        return _balance;

    }

    public void Withdraw(decimal amountToWithdraw)
    {
        if (amountToWithdraw <= _balance)
        {
            _balance -= amountToWithdraw;
        }
        else
        {
            throw new OverdraftException();
        }
    }
}

public class OverdraftException : ArgumentOutOfRangeException { }