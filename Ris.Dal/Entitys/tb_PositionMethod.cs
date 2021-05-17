using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ris.Dal.Entitys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tb_PositionMethod")]
    public partial class tb_PositionMethod
    {
           /// <summary>
           /// Desc:自增ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsIdentity=true)]
           public int ID {get;set;}

           /// <summary>
           /// Desc:部位/方法代码
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string Code {get;set;}

           /// <summary>
           /// Desc:部位/方法名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:是否是部位
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int? IsPosition {get;set;}

           /// <summary>
           /// Desc:方法的部位ID
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int? ParentID {get;set;}

           /// <summary>
           /// Desc:状态,1可用
           /// Default:1
           /// Nullable:False
           /// </summary>           
           public int Status {get;set;}

    }
}
