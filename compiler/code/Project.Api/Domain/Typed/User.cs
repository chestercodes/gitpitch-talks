﻿using System;

namespace Project.Api.Domain.Typed
{
    public class User
    {
        public User(UserId userId, KnownTenantId tenantId, Name name)
        {
            UserId = userId;
            TenantId = tenantId;
            
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name is null or empty");
            }
            Name = name;
        }

        public UserId UserId { get; }
        public KnownTenantId TenantId { get; }
        public Name Name { get; }
    }
}
