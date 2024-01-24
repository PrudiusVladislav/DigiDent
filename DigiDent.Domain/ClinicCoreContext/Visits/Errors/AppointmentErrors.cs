﻿using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Errors;

public static class AppointmentErrors
{
    public static Error PricePaidIsZeroWhenStatusIsComplete
    => new(
            ErrorType.Validation,
            nameof(Appointment),
            "Price paid parameter cannot be zero when visit status is set to 'Complete'.");
    
    public static Error PricePaidIsNotZeroWhenStatusIsNotComplete
        => new(
            ErrorType.Validation,
            nameof(Appointment),
            "Price paid parameter must be zero when visit status is not set to 'Complete'.");
}