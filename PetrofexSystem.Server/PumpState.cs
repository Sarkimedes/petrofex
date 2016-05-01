namespace PetrofexSystem.PosTerminals
{
    public enum PumpState
    {
        CustomerWaiting,
        ActivationPending,
        Active,
        AwaitingPayment,
        PaymentMade,
        Inactive,
        Error
    }
}