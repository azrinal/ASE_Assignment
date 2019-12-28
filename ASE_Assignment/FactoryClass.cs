using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Assignment
{
    class FactoryClass : Creator
    {
        public override IShape getShape(string shapeType)
        {
            shapeType = shapeType.ToLower().Trim(); //To get rid of Case Sensitivity


            if (shapeType.Equals("circle"))
            {
                return new Circle();

            }
            else if (shapeType.Equals("rectangle"))
            {
                return new Rectangle();

            }
            else if (shapeType.Equals("triangle"))
            {
                return new Triangle();

            }
            else
            {
                System.ArgumentException argEx = new System.ArgumentException("Factory error: " + shapeType + " does not exist");
                throw argEx;
            }
        }
    }
}
