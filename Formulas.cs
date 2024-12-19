using System;
using System.Collections.Generic;
using WebTeploobmenApp.Models;

namespace WebTeploobmenApp
{
    public static class Formulas
    {
        public class Parameter
        {
            public string Name { get; set; }
            public string Param { get; set; }
            public object? Value { get; set; }
            public Parameter(string name, string param, object? value)
            {
                Name = name;
                Param = param;
                Value = value;
            }
        }

        public class Data
        {
            public int H0 { get; set; }
            public double D { get; set; }
            public double T1 { get; set; }
            public double T2 { get; set; }
            public double Wr { get; set; }
            public double Cr { get; set; }
            public double Cm { get; set; }
            public double Gm { get; set; }
            public double Av { get; set; }

            public Data() { }
            public Data(Data copy)
            {
                H0 = copy.H0;
                D = copy.D;
                T1 = copy.T1;
                T2 = copy.T2;
                Wr = copy.Wr;
                Cr = copy.Cr;
                Cm = copy.Cm;
                Gm = copy.Gm;
                Av = copy.Av;
            }

            public static List<Parameter> GetParameters(Data? data = null) =>
            [
                new("Высота слоя", "h0", data?.H0),
                new("Начальная температура окатышей", "t1", data?.T1),
                new("Начальная температура воздуха", "t2", data?.T2),
                new("Скорость воздуха на свободное сечение шахты", "wr", data?.Wr),
                new("Средняя теплоёмкость воздуха при 200 градусах С", "cr", data?.Cr),
                new("Расход окатышей", "cm", data?.Cm),
                new("Теплоёмкость окатышей", "gm", data?.Gm),
                new("Диаметр аппарата", "d", data?.D),
                new("Величина альфа v", "av", data?.Av),
            ];
        }

        public static double CalcM(Data data) => (data.Gm * data.Cm) / (data.Wr * data.Cr * Math.PI * Math.Pow(data.D/2, 2));
        public static double CalcY0(Data data) => CalcY(data, data.H0);
        public static double CalcY(Data data, double y) => (data.Av * y) / (data.Wr * data.Cr * 1000);
        public static double Calc1exp(Data data, double y) => 1 - Math.Exp((CalcM(data) - 1) * CalcY(data, y) / CalcM(data));
        public static double Calc1mexp(Data data, double y) => 1 - CalcM(data)*Math.Exp((CalcM(data) - 1) * CalcY(data, y) / CalcM(data));
        public static double CalcV(Data data, double y) => Calc1exp(data, y) / Calc1mexp(data, data.H0);
        public static double CalcO(Data data, double y) => Calc1mexp(data, y) / Calc1mexp(data, data.H0);
        public static double CalcT1(Data data, double y) => data.T1 + (data.T2 - data.T1) * CalcV(data, y);
        public static double CalcT2(Data data, double y) => data.T1 + (data.T2 - data.T1) * CalcO(data, y);
        public static double CalcTDifference(Data data, double y) => CalcT1(data, y) - CalcT2(data, y);
    }
}
