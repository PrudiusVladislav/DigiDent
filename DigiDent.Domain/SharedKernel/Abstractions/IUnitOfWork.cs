﻿namespace DigiDent.Domain.SharedKernel.Abstractions;

public interface IUnitOfWork: IDisposable
{
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
}