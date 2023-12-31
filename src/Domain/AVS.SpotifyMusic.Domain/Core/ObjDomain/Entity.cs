﻿using AVS.SpotifyMusic.Domain.Core.Data;
using FluentValidation.Results;

namespace AVS.SpotifyMusic.Domain.Core.ObjDomain
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime DtCriacao { get; set; }
        public DateTime? DtAtualizacao { get; set; }
        public ValidationResult? ValidationResult { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            ValidationResult = new ValidationResult();
            DtCriacao = DateTime.Now;
        }

        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }
        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }
        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }
        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }

        public virtual void Validar() 
        {  
            throw new NotImplementedException(); 
        }
    }
}
