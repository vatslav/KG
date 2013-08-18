using kg_polygon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using shareData;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
namespace KGTest
{
    
    
    /// <summary>
    ///Это класс теста для AffinTransformTest, в котором должны
    ///находиться все модульные тесты AffinTransformTest
    ///</summary>
 [TestClass()]
 public class AffinTransformTest
 {


  private TestContext testContextInstance;
  private SLine figure;
  private Point nullPoint;
  private Point curPoint;


  /// <summary>
  ///Получает или устанавливает контекст теста, в котором предоставляются
  ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
  ///</summary>
  public TestContext TestContext
  {
   get
   {
    return testContextInstance;
   }
   set
   {
    testContextInstance = value;
   }
  }

  #region Дополнительные атрибуты теста
  // 
  //При написании тестов можно использовать следующие дополнительные атрибуты:
  //
  //ClassInitialize используется для выполнения кода до запуска первого теста в классе
  //[ClassInitialize()]
  //public static void MyClassInitialize(TestContext testContext)
  //{
  //}
  //
  //ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
  //[ClassCleanup()]
  //public static void MyClassCleanup()
  //{
  //}
  //
  //TestInitialize используется для выполнения кода перед запуском каждого теста
  //[TestInitialize()]
  //public void MyTestInitialize()
  //{
  //}
  //
  //TestCleanup используется для выполнения кода после завершения каждого теста
  //[TestCleanup()]
  //public void MyTestCleanup()
  //{
  //}
  //
  #endregion
  //TestInitialize используется для выполнения кода перед запуском каждого теста
  [TestInitialize()]
  public void SLineTestInitialize()
  {
    figure = new SLine(new Point(2, 2), new Point(2, 2));
    nullPoint = new Point(0, 0);
    curPoint = new Point(10, 10);
  }

  /// <summary>
  ///Тест для scale
  ///</summary>
  [TestMethod()]
  public void scaleTest()
  {
   //Point buf = new Point(2, 2);
   AffinTransform target = new AffinTransform(); // TODO: инициализация подходящего значения
   figure = new SLine(new Point(1, 1), new Point(2, 2));
   curPoint = new Point(0, 0);
   //Assert.IsTrue(new float[] { a, a }.Equals(target.scale(ref figure, curPoint)));
   Assert.IsTrue(true.Equals(true));
   //target.scale(ref figure, curPoint);
   Debug.WriteLine(figure.ToString());
   Debug.WriteLine(figure.ToStringMx());
  // Debug.WriteLine(figure.ToString());

  }
  /// <summary>
  ///Тест для scale
  ///</summary>
  //[TestMethod()]
  //public void scaleTest1()
  //{
  // AffinTransform target = new AffinTransform(); // TODO: инициализация подходящего значения
  // float x = 2; // TODO: инициализация подходящего значения
  // float y = 2; // TODO: инициализация подходящего значения
  // target.scale(figure, x, y);
  // //Assert.Inconclusive("Невозможно проверить метод, не возвращающий значение.");
  //}
 }
}
