using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ris.Dal.Entitys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("tb_LogInfo")]
    public partial class tb_LogInfo
    {
           public tb_LogInfo(){


           }
           /// <summary>
           /// Desc:自增id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ID {get;set;}

           /// <summary>
           /// Desc:记录日志的时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? LogTime {get;set;}

           /// <summary>
           /// Desc:日志类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LogType {get;set;}

           /// <summary>
           /// Desc:日志内容
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Content {get;set;}

           /// <summary>
           /// Desc:操作员工号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WorkID {get;set;}

    }
}
