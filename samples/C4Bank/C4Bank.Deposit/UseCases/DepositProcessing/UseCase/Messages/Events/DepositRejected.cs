using C4Bank.Deposit.Shared;

namespace C4Bank.Deposit.UseCases.DepositProcessing.UseCase.Messages.Events;

public record DepositRejected(string[] Reason): IEvent;