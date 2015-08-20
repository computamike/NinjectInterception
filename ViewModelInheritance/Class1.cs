using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ninject;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Infrastructure.Language;



namespace ViewModelInheritance
{

    public class ViewModel
    {
        public virtual int MyProperty { get; set; }
        public virtual DateTime NowTime()
        {
            return DateTime.Now;
        }
    }

    public class PageObject : ContextBoundObject
    {
        
    }
    public class CBOTest : ContextBoundObject
    {
    
        public void Go()
        {
            int k = 32;
            k *= 2;
        }
    }
    public class Test //: ContextBoundObject
    {

        public void Go()
        {
            int k = 32;
            k *= 2;
        }
    }

    [TestFixture]
    public class Tests
    {


        #region Ninject

        public class PageObjectViewModel : ViewModel, IInterceptor
        {

            public void Intercept(IInvocation invocation)
            {
                Console.WriteLine("METHOD INTERCEPTED - " + invocation.Request.Method.Name);
                Console.WriteLine("METHOD Type  - " + invocation.Request.Method.Name);

                switch (Type.GetTypeCode(invocation.Request.Method.ReturnType) )
                {
                    case TypeCode.Boolean:
                        invocation.ReturnValue = true;
                        break;
                    case TypeCode.Byte:
                        break;
                    case TypeCode.Char:
                        invocation.ReturnValue = 'c';
                        break;
                    case TypeCode.DBNull:
                        break;
                    case TypeCode.DateTime:
                        invocation.ReturnValue = DateTime.Parse("01/11/1955");
                        break;
                    case TypeCode.Decimal:
                        invocation.ReturnValue = 100;
                        break;
                    case TypeCode.Double:
                        invocation.ReturnValue = 200;
                        break;
                    case TypeCode.Empty:
                        invocation.ReturnValue = null;
                        break;
                    case TypeCode.Int16:
                        invocation.ReturnValue = 300;
                        break;
                    case TypeCode.Int32:
                        invocation.ReturnValue = 400;
                        break;
                    case TypeCode.Int64:
                        invocation.ReturnValue = 500;
                        break;
                    case TypeCode.Object:
                        invocation.ReturnValue = null;
                        break;
                    case TypeCode.SByte:
                        break;
                    case TypeCode.Single:
                        invocation.ReturnValue = 600;
                        break;
                    case TypeCode.String:
                        break;
                    case TypeCode.UInt16:
                        break;
                    case TypeCode.UInt32:
                        break;
                    case TypeCode.UInt64:
                        break;
                    default:
                        invocation.ReturnValue = null;
                        break;
                }

                
                
                
            }
        }
        
        
        /// <summary>
        /// Minimal Interceptor - just carries on
        /// </summary>
        public class CarryOnInterceptor : IInterceptor
        {
            public void Intercept(IInvocation invocation)
            {
                invocation.Proceed();
            }
        }

        
 
        
        #endregion


    [Test]
        public void NinjectAOPTest()
        {
            StandardKernel kernel = new StandardKernel();
            kernel.Bind<ViewModel>().To<PageObjectViewModel>().Intercept().With<PageObjectViewModel>();
            var foo = kernel.Get<ViewModel>();
            foo.MyProperty = 800;
            Console.WriteLine(foo.MyProperty.ToString());
            Console.WriteLine(foo.NowTime().ToString());
        }

        [Test]
        public void TestTheOverHeadofCBOs()
        {
            CBOTest t = new CBOTest();

            int starttime = Environment.TickCount;
            for (int i = 0; i < 5000000; i++)
                t.Go();
            int endtime = Environment.TickCount;
            Console.WriteLine((endtime - starttime).ToString());
            
        }
        [Test]
        public void TestTheOverHeadNonCBO()
        {
            Test t = new Test();

            int starttime = Environment.TickCount;
            for (int i = 0; i < 5000000; i++)
                t.Go();
            int endtime = Environment.TickCount;
            Console.WriteLine((endtime - starttime).ToString());

        }
    }


}
