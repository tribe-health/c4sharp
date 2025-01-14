using System.ComponentModel;
using C4Bank.Deposit.Shared;
using C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositoProcessing.UseCase;
using C4Bank.Deposit.UseCases.DepositProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositProcessing.UseCase.Messages.Commands;
using C4Bank.Deposit.UseCases.DepositProcessing.UseCase.Messages.Events;

namespace C4Bank.Deposit.UseCases.DepositProcessing.Adapters;

[Description("Deposit Worker API")]
public class DepositProcessingWorker: IWorker
{
    private readonly IDepositProcessingHandler _handler;

    public DepositProcessingWorker(IDepositProcessingHandler handler)
    {
        _handler = handler;
    }
    
    public async Task Consume(DepositReceived @event)
    {
        var command = DepositProcessingMapper.Map<RegisterDepositCommand>(@event);
        await _handler.ExecuteAsync(command);
    }
}