New-Service -Name "WorkerService_GoldDep" -BinaryPathName "D:\Ga\gold-aggregator-parser\GoldAggregator\GoldAggregator.Parser.WorkerServices\GoldAggregator.Parser.WorkerService.GoldDep\bin\Release\net6.0\GoldAggregator.Parser.WorkerService.GoldDep" -Description "WorkerService_GoldDep" -DisplayName "WorkerService_GoldDep" -StartupType Automatic

New-Service -Name "WorkerService_GoldDergava" -BinaryPathName "D:\Ga\gold-aggregator-parser\GoldAggregator\GoldAggregator.Parser.WorkerServices\GoldAggregator.Parser.WorkerService.GoldDergava\bin\Release\net6.0\GoldAggregator.Parser.WorkerService.GoldDergava" -Description "WorkerService_GoldDergava" -DisplayName "WorkerService_GoldDergava" -StartupType Automatic

New-Service -Name "WorkerService_IFK_Pik" -BinaryPathName "D:\Ga\gold-aggregator-parser\GoldAggregator\GoldAggregator.Parser.WorkerServices\GoldAggregator.Parser.WorkerService.IFK_Pik\bin\Release\net6.0\GoldAggregator.Parser.WorkerService.IFK_Pik" -Description "WorkerService_IFK_Pik" -DisplayName "WorkerService_IFK_Pik" -StartupType Automatic

New-Service -Name "WorkerService_MonetaInvestMsk" -BinaryPathName "D:\Ga\gold-aggregator-parser\GoldAggregator\GoldAggregator.Parser.WorkerServices\GoldAggregator.Parser.WorkerService.MonetaInvestMsk\bin\Release\net6.0\GoldAggregator.Parser.WorkerService.MonetaInvestMsk" -Description "WorkerService_MonetaInvestMsk" -DisplayName "WorkerService_MonetaInvestMsk" -StartupType Automatic

New-Service -Name "WorkerService_MonetaInvestSpb" -BinaryPathName "D:\Ga\gold-aggregator-parser\GoldAggregator\GoldAggregator.Parser.WorkerServices\GoldAggregator.Parser.WorkerService.MonetaInvestSpb\bin\Release\net6.0\GoldAggregator.Parser.WorkerService.MonetaInvestSpb" -Description "WorkerService_MonetaInvestSpb" -DisplayName "WorkerService_MonetaInvestSpb" -StartupType Automatic

New-Service -Name "WorkerService_NevaGold" -BinaryPathName "D:\Ga\gold-aggregator-parser\GoldAggregator\GoldAggregator.Parser.WorkerServices\GoldAggregator.Parser.WorkerService.NevaGold\bin\Release\net6.0\GoldAggregator.Parser.WorkerService.NevaGold" -Description "WorkerService_NevaGold" -DisplayName "WorkerService_NevaGold" -StartupType Automatic

New-Service -Name "WorkerService_ZolotoMDRu" -BinaryPathName "D:\Ga\gold-aggregator-parser\GoldAggregator\GoldAggregator.Parser.WorkerServices\GoldAggregator.Parser.WorkerService.ZolotoMdRu\bin\Release\net6.0\GoldAggregator.Parser.WorkerService.ZolotoMdRu" -Description "WorkerService_ZolotoMDRu" -DisplayName "WorkerService_ZolotoMDRu" -StartupType Automatic

New-Service -Name "WorkerService_Numizmatik" -BinaryPathName "D:\Ga\gold-aggregator-parser\GoldAggregator\GoldAggregator.Parser.WorkerServices\GoldAggregator.Parser.WorkerService.Numizmatik\bin\Release\net6.0\GoldAggregator.Parser.WorkerService.Numizmatik" -Description "WorkerService_Numizmatik" -DisplayName "WorkerService_Numizmatik" -StartupType Automatic

New-Service -Name "WorkerService_Petroinvest" -BinaryPathName "D:\Ga\gold-aggregator-parser\GoldAggregator\GoldAggregator.Parser.WorkerServices\GoldAggregator.Parser.WorkerService.Petroinvest\bin\Release\net6.0\GoldAggregator.Parser.WorkerService.Petroinvest" -Description "WorkerService_Petroinvest" -DisplayName "WorkerService_Petroinvest" -StartupType Automatic

New-Service -Name "WorkerService_ZolotoPiterRu" -BinaryPathName "D:\Ga\gold-aggregator-parser\GoldAggregator\GoldAggregator.Parser.WorkerServices\GoldAggregator.Parser.WorkerService.ZolotoPiterRu\bin\Release\net6.0\GoldAggregator.Parser.WorkerService.ZolotoPiterRu" -Description "WorkerService_ZolotoPiterRu" -DisplayName "WorkerService_ZolotoPiterRu" -StartupType Automatic

New-Service -Name "WorkerService_TroyStandart" -BinaryPathName "D:\Ga\gold-aggregator-parser\GoldAggregator\GoldAggregator.Parser.WorkerServices\GoldAggregator.Parser.WorkerService.TroyStandart\bin\Release\net6.0\GoldAggregator.Parser.WorkerService.TroyStandart" -Description "WorkerService_TroyStandart" -DisplayName "WorkerService_TroyStandart" -StartupType Automatic

 Start-Service WorkerService_GoldDep
 Start-Service WorkerService_GoldDergava
 Start-Service WorkerService_IFK_Pik
 Start-Service WorkerService_MonetaInvestMsk
 Start-Service WorkerService_MonetaInvestSpb
 Start-Service WorkerService_NevaGold
 Start-Service WorkerService_ZolotoMDRu
 Start-Service WorkerService_Numizmatik
 Start-Service WorkerService_Petroinvest
 Start-Service WorkerService_ZolotoPiterRu
 Start-Service WorkerService_TroyStandart