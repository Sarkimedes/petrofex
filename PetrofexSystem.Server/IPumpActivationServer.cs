﻿namespace PetrofexSystem.Server
{
    /// <summary>
    /// Represents the server used to activate pumps
    /// </summary>
    public interface IPumpActivationServer
    {
        /// <summary>
        /// Requests that the pump with the given ID is activated.
        /// </summary>
        /// <param name="pumpId">The pump identifier.</param>
        void RequestActivation(string pumpId);

        /// <summary>
        /// Requests that the pump with the given ID is deactivated.
        /// </summary>
        /// <param name="pumpId">The pump identifier.</param>
        void RequestDeactivation(string pumpId);
    }
}