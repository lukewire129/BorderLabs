# BorderLabs

### Overview
정적인 Border를 동적으로 변경할 수 있는 다양한 시도를 통해 만들어내기 위한 저장소입니다.

## Border 상속기반
### 1. Animation Loop Mode

 ![image](https://github.com/lukewire129/BorderLabs/assets/54387261/986ebfc1-9cb1-462a-8356-3867634c7b38)

- 테두리 색상 원리
 **BorderBrushs**는 값이 업데이트 될 때마다 값에 따라 비율 조절하고 LinearGradation기반으로 만들어냈습니다
- 구현 방식
  지정된 시간에 맞게 StartPoint와 EndPoint를 360도 값을 변경하며 회전하게 됩니다.

**SAMPLE CODE**
```csharp
 <units:AnimateBorder
     AnimationType="Loop"
     BorderThickness="10"
     CornerRadius="50"
     Interval="0:0:2">
     <units:AnimateBorder.BorderBrushs>
         <x:Array Type="{x:Type SolidColorBrush}">
             <SolidColorBrush Color="Blue" />
             <SolidColorBrush Color="Red" />
             <SolidColorBrush Color="Lime" />
             <SolidColorBrush Color="Black" />
         </x:Array>
     </units:AnimateBorder.BorderBrushs>
     <Grid>
         <Grid.ColumnDefinitions>
             <ColumnDefinition Width="100" />
             <ColumnDefinition Width="auto" />
         </Grid.ColumnDefinitions>
         <Label
             HorizontalAlignment="Center"
             VerticalAlignment="Center"
             Content="Box" />
         <Button
             Grid.Column="1 "
             Width="100"
             Height="50" />
     </Grid>
 </units:AnimateBorder>
```

| WPF | MAUI |
|:----:|:----:|
|<video src="https://github.com/lukewire129/BorderLabs/assets/54387261/b8ea25e6-f5ac-4815-a405-cf821a8180b0"/> | <video src="https://github.com/lukewire129/BorderLabs/assets/54387261/4da23973-d2ba-4bc6-ac79-4331ed325eca"/>|

### 2. Animation FadeInOut Mode
- 구현 방식
  Border 자체를 Opacity로 제어하는 경우에는 그 안에 속한 하위 계층들 또한 Opacity 속성이 들어가기 때문에 Color값으로 지정된 컬러 -> Tranparent로 변경하는 방식으로 구현
**SAMPLE CODE**
```csharp
<units:AnimateBorder
    AnimationType="FadeInOut"
    BorderBrush="Blue"
    BorderThickness="5"
    CornerRadius="3"
    Interval="0:0:0.5">
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="100" />
            <ColumnDefinition Width="auto" />
        </Grid.ColumnDefinitions>
        <Label
            HorizontalAlignment="Center"
            VerticalAlignment="Center"
            Content="Box" />
        <Button
            Grid.Column="1 "
            Width="100"
            Height="50" />
    </Grid>
</units:AnimateBorder>
```
https://github.com/lukewire129/BorderLabs/assets/54387261/54812961-e6d6-4fbc-85aa-e33f453b2628

