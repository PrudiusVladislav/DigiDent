﻿using MediatR;

namespace DigiDent.Shared.Abstractions.Messaging;

/// <summary>
/// Defines an integration event.
/// </summary>
public interface IIntegrationEvent: INotification
{
    Guid EventId { get; }
    DateTime TimeOfOccurrence { get; }
}