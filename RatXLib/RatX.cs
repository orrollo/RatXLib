using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntXLib;

namespace RatXLib
{
	public class RatX
	{
		public IntX Numer { get; set; }
		public IntX Denom { get; set; }

		public RatX(IntX num, IntX denom)
		{
			Numer = num;
			Denom = denom;
			RatXHelper.Norm(this);
		}

		public RatX(IntX num) : this(num, 1)
		{

		}

		public RatX() : this(0, 1)
		{

		}

		// 
		public override string ToString()
		{
			return string.Format("{0} / {1}", Numer, Denom);
		}

		//

		public static implicit operator RatX(IntX value)
		{
			return new RatX(value);
		}

		public static implicit operator RatX(int value)
		{
			return new RatX(new IntX(value));
		}

		public static implicit operator RatX(uint value)
		{
			return new RatX(new IntX(value));
		}

		public static implicit operator RatX(long value)
		{
			return new RatX(new IntX(value));
		}

		public static implicit operator RatX(ulong value)
		{
			return new RatX(new IntX(value));
		}

		//

		public static RatX operator +(RatX a, RatX b)
		{
			return RatXHelper.AddSub(a, b);
		}

		public static RatX operator -(RatX a, RatX b)
		{
			return RatXHelper.AddSub(a, b, false);
		}

		public static RatX operator *(RatX a, RatX b)
		{
			return RatXHelper.Mul(a, b);
		}

		public static RatX operator /(RatX a, RatX b)
		{
			return RatXHelper.Div(a, b);
		}
	}

	internal static class RatXHelper
	{
		public static IntX Gcd(IntX a, IntX b)
		{
			IntX t;
			if (b > a)
			{
				t = a;
				a = b;
				b = t;
			}
			if (b == 0) return 1;
			while (b != 0)
			{
				t = b;
				b = a%b;
				a = t;
			}
			return a;
		}

		public static RatX Norm(RatX a)
		{
			if (a.Denom < 0)
			{
				a.Denom = -a.Denom;
				a.Numer = -a.Numer;
			}
			return a;
		}

		public static RatX AddSub(RatX a, RatX b, bool isAdd = true)
		{
			IntX n, d, g;
			a = Norm(a);
			b = Norm(b);
			if (a.Denom == b.Denom)
			{
				d = a.Denom;
				n = isAdd 
					? a.Numer + b.Numer 
					: a.Numer - b.Numer;
			}
			else
			{
				g = Gcd(a.Denom, b.Denom);
				if (g != 1)
				{
					IntX da = a.Denom/g, db = b.Denom/g;
					d = da*db*g;
					n = isAdd 
						? a.Numer * db + b.Numer * da 
						: a.Numer * db - b.Numer * da;
				}
				else
				{
					d = a.Denom * b.Denom;
					n = isAdd 
						? a.Numer * b.Denom + b.Numer * a.Denom 
						: a.Numer * b.Denom - b.Numer * a.Denom;
				}
			}
			g = Gcd(d, n);
			if (g != 1)
			{
				d = d / g;
				n = n / g;
			}
			return new RatX(n, d);
		}

		public static RatX Mul(RatX a, RatX b)
		{
			a = Norm(a);
			b = Norm(b);
			IntX n = a.Numer*b.Numer, d = a.Denom*b.Denom, g = Gcd(n, d);
			if (g != 1)
			{
				n = n/g;
				d = d/g;
			}
			return new RatX(n, d);
		}

		public static RatX Div(RatX a, RatX b)
		{
			a = Norm(a);
			b = Norm(b);
			IntX n = a.Numer * b.Denom, d = a.Denom * b.Numer, g = Gcd(n, d);
			if (g != 1)
			{
				n = n / g;
				d = d / g;
			}
			return new RatX(n, d);
		}
	}
}
