using C4Bank.Deposit.UseCases.DepositoProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositProcessing.Interfaces;
using C4Bank.Deposit.UseCases.DepositProcessing.UseCase.Messages.Commands;
using C4Bank.Deposit.UseCases.DepositProcessing.UseCase.Messages.Events;

namespace C4Bank.Deposit.UseCases.DepositoProcessing.UseCase;

/// <summary>
/// This class represents the use of Decorator pattern
/// <see href="https://refactoring.guru/design-patterns/decorator"/>
/// </summary>
public class DepositProcessingValidation: IDepositProcessingHandler
{
    private readonly IDepositProcessingHandler _handler;
    private readonly IDepositoProcessingProducer _producer;

    public DepositProcessingValidation(IDepositProcessingHandler handler, IDepositoProcessingProducer producer)
    {
        _handler = handler;
        _producer = producer;
    }
    
    public async Task ExecuteAsync(RegisterDepositCommand command)
    {
        var (success, messages) = Validate(command);

        if (!success)
        {
            var @event = DepositProcessingMapper.Map<DepositRejected>(command);
            await _producer.Reject(@event with { Reason = messages });
        }

        await _handler.ExecuteAsync(command);
    }

    private (bool success, string[] messages) Validate(RegisterDepositCommand command)
    {
        return (true, Array.Empty<string>());
    }
}