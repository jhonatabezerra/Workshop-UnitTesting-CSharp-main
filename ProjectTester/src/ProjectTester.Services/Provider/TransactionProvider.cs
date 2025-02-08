using ProjectTester.Domain.Models.Enum;
using ProjectTester.Domain.Models.Operations;

namespace ProjectTester.Services.Provider
{
    /// <inheritdoc/>
    public class TransactionProvider : ITransactionProvider
    {
        /// <inheritdoc/>
        public Operations GetOperation() => new()
        {
            Operation = OperationTypes.BUY,
            UnitCost = 1,
            Quantity = 1,
        };

        /// <inheritdoc/>
        public Operations PostOperation(Operations operations)
        {
            Operations operations1 = operations;
            return operations1;
        }
    }

    /// <summary>Responsible for provider the <see cref="Operations"/> information.</summary>
    public interface ITransactionProvider
    {
        /// <summary>Get a new <see cref="Operations"/> instance with information.</summary>
        /// <returns>A <see cref="Operations"/> instance.</returns>
        /// 
        Operations GetOperation();
        /// <summary>Get a new <see cref="Operations"/> instance with information.</summary>
        /// <returns>A <see cref="Operations"/> instance.</returns>
        Operations PostOperation(Operations operations);
    }
}