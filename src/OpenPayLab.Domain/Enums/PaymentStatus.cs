using System;

namespace OpenPayLab.Domain.Enums;

public enum PaymentStatus
{
    Pending,	// Pago creado, pendiente de procesar.
    Authorized,	// El proveedor ha autorizado el pago.
    Captured,	// El importe ha sido cobrado.
    Failed,	    // El pago ha fallado definitivamente.
    Cancelled,	// El pago ha sido cancelado.
}
