﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects;
public record ProductId
{
    public Guid Value { get; }

    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Product Id cannot be empty");
        }
        return new ProductId(value);
    }
}
