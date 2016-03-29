# BAH.Solution
Make Easy For K/3 Cloud

//这是通常的数据包访问形式。
var materialId = (int)((this.Model.GetValue("FMaterialId") as DynamicObject)["Id"]);

//这是引用扩展后的访问形式。
var materialId = this.Model.GetValue("FMaterialId").ToType<DynamicObject>().PkId<int>();


//如果上述案例不够有说服力，请往下看：
//假如从一个普通的业务单据上，取物料字段的库存单位编码，
//通常是这样访问数据包的。
var material = this.Model.GetValue("FMaterialId") as DynamicObject;
var materialStock = (material["MaterialStock"] as DynamicObjectCollection).FirstOrDefault();
var storeUnit = materialStock["StoreUnitId"] as DynamicObject;
var storeUnitNumber = (string)storeUnit["Number"];

//引用扩展后访问一条龙，中间不带喘气，
//写Linq或Lambda更加从容不迫。
var storeUnitNumber = this.Model.GetValue("FMaterialId").ToType<DynamicObject>()
                          .Property<DynamicObjectCollection>("MaterialStock").FirstOrNullDefault()
                          .Property<DynamicObject>("StoreUnitId").BDNumber();

                            