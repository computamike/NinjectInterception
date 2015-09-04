using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ninject;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Infrastructure.Language;
using System.Collections;
using System.Web.Mvc;
using Moq;
using System.IO;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc.Html;
using System.Linq.Expressions;


namespace ViewModelInheritance
{

    public class ViewModel
    {
        public virtual int MyProperty { get; set; }
        public virtual DateTime NowTime()
        {
            return DateTime.Now;
        }
        public virtual List<thing> things { get; set; }
        public virtual VM2 Level2 { get; set; }

    }
    public class VM2
    {
        public virtual string Level2String { get; set; }
        public virtual VM3 Level3 { get; set; }
    }
    public class VM3
    {
        public virtual string Level3String { get; set; }
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

    public class FakeList<T> : IList<T>,  IList, IReadOnlyList<T>
    {
        private List<T> thingies = new List<T>();
        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
             thingies.CopyTo(array ,arrayIndex);
            
        }

        public int Count
        {
            get { return 99; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        object IList.this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }
    }

 
    [TestFixture]
    public class Tests
    {
 
        [Test]
        public void TestGenericTypeMethodCall()
        {
            var v = new ViewModel();
            v.MyProperty = 99;
            v.things = new List<thing>();
            v.things.Add(new thing() { x = 10, y = 10, z = 10 });
            v.Level2 = new VM2();
            v.Level2.Level2String = "Level2";
            v.Level2.Level3 = new VM3();
            v.Level2.Level3.Level3String = "Level3";
            var typeToFind = v.Level2.Level3.Level3String.GetType();
            var PropertyInfo = v.GetType().GetProperty("Level2.Level3.Level3String");
             

            var h = new HtmlHelper<ViewModel>(new ViewContext(),new FakeViewDataContainer());

            var Sid = h.IdFor(m => m.things[2].x);


            StandardKernel kernel = new StandardKernel();
            kernel.Bind<ViewModel>().To<PageObjectViewModel>().Intercept().With<PageObjectViewModel>();
            var foo = kernel.Get<ViewModel>();

         
        
        }
        #region Ninject

        public class PageObjectViewModel : ViewModel, IInterceptor
        {

            public  MvcHtmlString WIPIdFor<TModel, TProperty>( Expression<Func<TModel, TProperty>> expression)
            {
                return new  MvcHtmlString("blah");
            }

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
                        var tmp = invocation.Request.Method.ReturnType;
                    
                        if (tmp.GetInterface("IList") !=null)
                        {

                            var foo = (IList<thing>)new FakeList<thing>();
                            System.Collections.Generic.List<thing> foo2 =  foo.ToList<thing>() ;
                            invocation.ReturnValue = foo2;
                        }
                        else
                        {
                            invocation.ReturnValue = null;
                        }
                        
                        
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


        public class FakeViewDataContainer : IViewDataContainer
        {
            private ViewDataDictionary _viewData = new ViewDataDictionary();
            public ViewDataDictionary ViewData { get { return _viewData; } set { _viewData = value; } }
        }


        [Test]
        public void CreateNameForModel()
        {
            // Let's start thinking about naming conventions
            //var v = new ViewModel();
            
            //var vt = v.GetType();
            //var vd = new ViewDataDictionary();
            //var helper = CreateHtmlHelper(vd);
            ////var html = new System.Web.Mvc.HtmlHelper<ViewModel>(new System.Web.Mvc.ViewContext() { ViewData = new System.Web.Mvc.ViewDataDictionary<ViewModel>(this._test) }, this.IVDC);
            ////HtmlHelper<ViewModel> helper = null;
             
            //System.Linq.Expressions.Expression<Func<ViewModel,int>> expression = (a) => a.MyProperty;
            //var nme = System.Web.Mvc.Html.NameExtensions.IdFor<ViewModel, int>(helper, (a) => a.MyProperty);

            ////var s = System.Web.Mvc.Html.EditorExtensions.EditorFor<ViewModel, Int32>(html, expression);

            //Console.Write(nme);
        }

        public static HtmlHelper CreateHtmlHelper(ViewDataDictionary vd, Stream stream = null)
        {
            TextWriter textWriter = new StreamWriter(stream ?? new MemoryStream());
            Mock<ViewContext> mockViewContext = new Mock<ViewContext>(
                new ControllerContext(
                    new Mock<HttpContextBase>().Object,
                    new RouteData(),
                    new Mock<ControllerBase>().Object
                ),
                new Mock<IView>().Object,
                vd,
                new TempDataDictionary(),
                textWriter
            );
            mockViewContext.Setup(vc => vc.Writer).Returns(textWriter);

            Mock<IViewDataContainer> mockDataContainer = new Mock<IViewDataContainer>();
            mockDataContainer.Setup(c => c.ViewData).Returns(vd);

            return new HtmlHelper(mockViewContext.Object, mockDataContainer.Object);
        }


    [Test]
        public void NinjectAOPTest()
        {
            StandardKernel kernel = new StandardKernel();
            kernel.Bind<ViewModel>().To<PageObjectViewModel>().Intercept().With<PageObjectViewModel>();
            var foo = kernel.Get<ViewModel>();
            foo.MyProperty = 800;
            var f = foo.things.Count;
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

        public System.Web.Mvc.ViewDataDictionary _test { get; set; }

        public System.Web.Mvc.IViewDataContainer IVDC { get; set; }
    }


}
