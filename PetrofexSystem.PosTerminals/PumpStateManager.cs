using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals
{
    public class PumpStateManager
    {
        public PumpStateManager()
        {
            this.CurrentState = PumpState.Inactive;
        }

        public PumpState CurrentState { get; private set; }

        public void SetState(PumpState newState)
        {
            if (newState == PumpState.Error || this.CurrentState == PumpState.Error)
            {
                this.CurrentState = newState;
                return;
            }

            if (this.CurrentState == newState)
            {
                return;
            }

            PumpState[] allowedStates = null;
            switch (CurrentState)
            {
                case PumpState.Inactive:
                    allowedStates = new[] { PumpState.CustomerWaiting };
                    break;
                case PumpState.CustomerWaiting:
                    allowedStates = new[] { PumpState.ActivationPending, PumpState.Inactive };
                    break;
                case PumpState.ActivationPending:
                    allowedStates = new[] { PumpState.Active, PumpState.Inactive };
                    break;
                case PumpState.Active:
                    allowedStates = new[] { PumpState.AwaitingPayment };
                    break;
                case PumpState.AwaitingPayment:
                    allowedStates = new[] { PumpState.PaymentMade };
                    break;
                case PumpState.PaymentMade:
                    allowedStates = new[] { PumpState.Inactive };
                    break;
                default:
                    throw new NotImplementedException(string.Format("A transition to the state {0} has not been implemented yet.", newState));
            }

            SetOrThrow(newState, allowedStates);
        }

        private void SetOrThrow(PumpState newState, IEnumerable<PumpState> allowedStates)
        {            
            if (allowedStates != null && allowedStates.Any(x => x == newState))
            {
                this.CurrentState = newState;
            }
            else
            {
                ThrowExceptionForInvalidTransition(newState);
            }

        }

        private void ThrowExceptionForInvalidTransition(PumpState newState)
        {
            throw new InvalidOperationException(string.Format("Cannot set a pump in state {0} to state {1}.", this.CurrentState, newState));
        }
    }
}
