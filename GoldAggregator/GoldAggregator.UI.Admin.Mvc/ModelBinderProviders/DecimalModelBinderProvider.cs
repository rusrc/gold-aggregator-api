using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace GoldAggregator.UI.Admin.Mvc.ModelBinderProviders
{
    public class DecimalModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));

            // our binders here
            if (context.Metadata.ModelType == typeof(object))
            {
                return new BinderTypeModelBinder(typeof(DecimalModelBinder));
            }

            // your maybe have more binders?
            // ....

            // this provider does not provide any binder for given type
            //   so we return null
            return null;
        }
    }
}
