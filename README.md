**WPF, Maui Border PointAnimation**


## Animation Loop Mode

**Color Setting**

![image](https://github.com/lukewire129/BorderLabs/assets/54387261/986ebfc1-9cb1-462a-8356-3867634c7b38)

**Sample Code**
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

## Animation FadeInOut Mode
**Sample Code**
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

