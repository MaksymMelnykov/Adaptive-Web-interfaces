using System.Reflection;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Footballer footballer = new Footballer("Jude", 21, "Midfielder", "Real Madrid", 100);

            Console.WriteLine("Type and TypeInfo: ");
            Type type = footballer.GetType();
            TypeInfo typeInfo = type.GetTypeInfo();

            Console.WriteLine($"Type: {type}, Name: {type.Name}, Full Name: {type.FullName}, Namespace: {type.Namespace}, " +
                $"Attributes: {type.Attributes}, Assembly: {type.Assembly}\n");

            Console.WriteLine($"TypeInfo Name: {typeInfo.Name}, Full Name: {typeInfo.FullName}");
            Console.WriteLine($"Is Abstract: {typeInfo.IsAbstract}");
            Console.WriteLine($"Is Sealed: {typeInfo.IsSealed}");
            Console.WriteLine($"Is Interface: {typeInfo.IsInterface}");
            Console.WriteLine($"Base Type: {typeInfo.BaseType}");
            Console.WriteLine($"IsGenericType: {typeInfo.IsGenericType}");
            Console.WriteLine($"IsValueType: {typeInfo.IsValueType}");
            Console.WriteLine($"IsClass: {typeInfo.IsClass}");

            Console.WriteLine("\nMemberInfo: ");
            MemberInfo[] members = typeInfo.GetMembers();
            Console.Write("\nThere are {0} members in ", members.GetLength(0));
            foreach ( var member in members )
            {
                Console.WriteLine($"{member.MemberType}: {member.Name}, ReflectedType: {member.ReflectedType}, Module: {member.Module}");
            }

            Console.WriteLine("\nFieldInfo: ");
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
            foreach (var field in fields)
            {
                Console.WriteLine($"Field: {field.Name}, Value: {field.GetValue(field.IsStatic ? null : footballer)}, Type: {field.FieldType}, " +
                    $"Attributes: {field.Attributes}");
            }

            Console.WriteLine("\nMethodInfo:");
            MethodInfo[] methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
            foreach (var method in methods)
            {
                Console.WriteLine($"Name: {method.Name}, Attributes: {method.Attributes}, ReturnParameter: {method.ReturnParameter}, " +
                    $"ReturnType: {method.ReturnType}, IsPublic: {method.IsPublic}");
            }

            Console.WriteLine();

            MethodInfo scoreGoal = type.GetMethod("ScoreGoal");
            MethodInfo celebrate = type.GetMethod("Celebrate", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo transfer = type.GetMethod("Transfer", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo retirement = type.GetMethod("Retirement");

            scoreGoal.Invoke(footballer, null);
            celebrate.Invoke(footballer, null);
            transfer.Invoke(footballer, new object[] { "Barcelona" });
            retirement.Invoke(footballer, null);
        }
    }
}
