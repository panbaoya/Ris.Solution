using Nursing.Tools;
using Ris.Dal.Entitys;
using Ris.Dal.EntityService;
using Ris.IBll;
using Ris.Models.PositionMethod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ris.Bll
{
    public class PositionMethodBll : IPositionMethodBll
    {
        PositionMethodService _positionMethodService;
        public PositionMethodBll()
        {
            _positionMethodService = new PositionMethodService();
        }

        public List<PositionMethodModel> GetPositionMethods()
        {
           return _positionMethodService.GetPositionMethods().MapListTo<tb_PositionMethod,PositionMethodModel>();
        }

        public bool AddPositionMethod(PositionMethodModel model)
        {
            var entity = model.MapTo<tb_PositionMethod>();
            return _positionMethodService.Insert(entity)>0;
        }

        public bool UpdatePositionMethod(PositionMethodModel model)
        {
            var entity = _positionMethodService.GetById(model.ID);
            entity = model.MapTo<PositionMethodModel, tb_PositionMethod>(entity);
            return _positionMethodService.Update(entity);
        }

        public List<PositionMethodModel> GetPositions()
        {
            return _positionMethodService.GetPositions().MapListTo<tb_PositionMethod, PositionMethodModel>();
        }

        public List<PositionMethodModel> GetMethods(int? positionID = null)
        {
            return _positionMethodService.GetMethods(positionID).MapListTo<tb_PositionMethod, PositionMethodModel>();
        }

        public Task AddPositionMethodByHisAsync(List<PositionMethodModel> models)
        {
            return Task.Run(() =>
            {
                models.ForEach(x =>
                {
                    if (!_positionMethodService.IsExist(x.Code))
                    {
                        var entity = x.MapTo<tb_PositionMethod>();
                        _positionMethodService.Insert(entity);
                    }
                });
            });
        }
    }
}
