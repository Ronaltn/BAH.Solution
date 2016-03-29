# BAH.Solution——来自K/3 Cloud伙伴的解决方案

这是通常的数据包访问形式。
```cs
var materialId = (int)((this.Model.GetValue("FMaterialId") as DynamicObject)["Id"]);
```


这是引用扩展后的访问形式。
```cs
var materialId = this.Model.GetValue("FMaterialId").ToType<DynamicObject>().PkId<int>();
```



假如从一个普通的业务单据上，取物料字段的库存单位编码，通常是这样访问数据包的。
```cs
var material = this.Model.GetValue("FMaterialId") as DynamicObject;
var materialStock = (material["MaterialStock"] as DynamicObjectCollection).FirstOrDefault();
var storeUnit = materialStock["StoreUnitId"] as DynamicObject;
var storeUnitNumber = (string)storeUnit["Number"];
```

引用扩展后访问一条龙，中间不带喘气，写Linq或Lambda更加从容不迫。
```cs
var storeUnitNumber = this.Model.GetValue("FMaterialId").ToType<DynamicObject>()
                          .Property<DynamicObjectCollection>("MaterialStock").FirstOrNullDefault()
                          .Property<DynamicObject>("StoreUnitId").BDNumber();
```