using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace User.Domain.Enumerations
{
    [method: JsonConstructor]
    public class UserStatus(string name, int value) : SmartEnum<UserStatus>(name, value)
    {
        public static readonly UserStatus Default = new DefaultStatus();
        public static readonly UserStatus Active = new ActiveStatus();
        public static readonly UserStatus Blocked = new BlockedStatus();
        public static readonly UserStatus Defaulter = new DefaulterStatus();

        public static implicit operator UserStatus(string name)
            => FromName(name);

        public static implicit operator UserStatus(int value)
            => FromValue(value);

        public static implicit operator string(UserStatus status)
            => status.Name;

        public static implicit operator int(UserStatus status)
            => status.Value;

        public class DefaultStatus() : UserStatus(nameof(Default), 0) { }
        public class ActiveStatus() : UserStatus(nameof(Active), 1) { }
        public class BlockedStatus() : UserStatus(nameof(Blocked), 2) { }
        public class DefaulterStatus() : UserStatus(nameof(Defaulter), 3) { }
    }
}
