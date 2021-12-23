using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace WpfStudy
{
    public class ViewModelLocator
    {
        //사용을 원하는 ViewModel을 선언한다
        //우선, 이 파일을 App.xaml에 등록한다
        //추가적으로 여기에 작성한 것들은 App.xaml.cs 파일에 등록해야 사용이 가능하다
        public ViewModel VM => Ioc.Default.GetService<ViewModel>();
    }
}
