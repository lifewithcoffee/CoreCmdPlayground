# CoreCmdPlayground.CefSharp

- For loading symbols in vscode, see the comment for element
  `CefSharpAnyCpuSupport` in `CoreCmdPlayground.CefSharp.csproj`
- For setting platform when do "dotnet build" see `watch.cmd`.
- Save source and screenshot, see method `BrowserLoadingStateChanged`

## Troubleshooting Record

- **Error details:**
   
	> Could not load file or assembly '...\bin\x64\Debug\net7.0\runtimes\win-x64\lib\netcoreapp3.1\CefSharp.Core.Runtime.dll'.
	  The specified module could not be found.
	
  **Resolve1:**

	Change `CefSharp.OffScreen` in:
	``` html
	<Reference Update="CefSharp.OffScreen">
		<Private>true</Private>  
	</Reference>
	```

	to `CefSharp.OffScreen.NETCore`:
	``` html
	<Reference Update="CefSharp.OffScreen.NETCore">
		<Private>true</Private>  
	</Reference>
	```
  
	**Resolve2:**

	Just remove
	``` html
	<Reference Update="CefSharp.OffScreen">
		<Private>true</Private>  
	</Reference>
	```
