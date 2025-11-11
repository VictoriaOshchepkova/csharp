using System;
using System.Collections.Generic;

namespace ConsoleApp4_1
{
    class Money
    {
        uint _rubles;
        byte _kopeks;
        public uint Rubles
        {
            get { return _rubles; }
            set { _rubles = value; }
        }

        public byte Kopeks
        {
            get { return _kopeks; }
            set { _kopeks = value; }
        }

        public Money() 
        {
            Rubles = 0;
            Kopeks = 0;
        }

        public Money(uint rubles, byte kopeks) 
        {
            Rubles = rubles;
            Kopeks = kopeks;
            Normalize();
        }

        public override string ToString()
        {
            return $"{Rubles} руб. {Kopeks} коп.";
        }

        public Money Subtract(Money other)
        {
            if (other == null)
                throw new ArgumentException(nameof(other));

            long totalKopeksThis = (long)Rubles * 100 + Kopeks;
            long totalKopeksOther = (long)other.Rubles * 100 + other.Kopeks;
            long resultKopeks = totalKopeksThis - totalKopeksOther;

            if (resultKopeks < 0)
                throw new Exception("Error: The result of subtracting a monetary value cannot be negative");

            uint resultRubles = (uint)(resultKopeks / 100);
            byte resultKopeksByte = (byte)(resultKopeks % 100);

            return new Money(resultRubles, resultKopeksByte);
        }

        private void Normalize()
        {
            if (Kopeks >= 100)
            {
                Rubles += (uint)(Kopeks / 100);
                Kopeks = (byte)(Kopeks % 100);
            }
        }

        public static Money operator ++(Money money)
        {
            if (money == null)
                throw new ArgumentException("Error: a Money type parameter is expected");

            byte newKopeks = (byte)(money.Kopeks + 1);
            uint newRubles = money.Rubles;

            if (newKopeks >= 100)
            {
                newRubles++;
                newKopeks = 0;
            }

            return new Money(newRubles, newKopeks);
        }

        public static Money operator --(Money money)
        {
            if (money == null)
                throw new ArgumentException("Error: A Money type parameter is expected");

            if (money.Rubles == 0 && money.Kopeks == 0)
                throw new ArgumentException("Error: A zero monetary amount cannot be reduced");

            byte newKopeks = money.Kopeks;
            uint newRubles = money.Rubles;

            if (newKopeks == 0)
            {
                newRubles--;
                newKopeks = 99;
            }
            else
            {
                newKopeks--;
            }

            return new Money(newRubles, newKopeks);
        }

        public static implicit operator uint(Money money)
        {
            if (money == null)
                throw new ArgumentException("Error: A Money type parameter is expected");
            
            return money.Rubles; 
        }

        public static explicit operator double(Money money)
        {
            if (money == null)
                throw new ArgumentException("Error: A Money type parameter is expected");
            
            return money.Kopeks / 100.0; 
        }

        public static Money operator -(Money left, Money right)
        {
            if (left == null || right == null)
                throw new ArgumentException("Error: 2 Money type parameters are expected");

            return left.Subtract(right);
        }

        public static Money operator -(Money money, uint rubles)
        {
            if (money == null)
                throw new ArgumentException("Error: A Money type parameter is expected");

            Money other = new Money(rubles, 0);
            return money - other;
        }

        public static Money operator -(uint rubles, Money money)
        {
            if (money == null)
                throw new ArgumentException("Error: A Money type parameter is expected");

            Money other = new Money(rubles, 0);
            return other - money;
        }
    }
}
