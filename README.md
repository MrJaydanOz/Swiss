# Swiss

The the code is on the [development branch](https://github.com/MrJaydanOz/Swiss/tree/development). (I was trying to be
clever by starting the repo with a 'setup' branch)

Swiss is my personal in-development Unity library that is designed to make programming more convenient. It will\already
contains overloaded functions for common math operations such as `Min` or `Clamp`, and some additional uncommon operations
such as `Avoid` or `LineIntersect`.

The main part of this library is the template system. Inspired by C++'s '`template<>`' and Typescripts '`.d.ts`', the
'`.t.cs`' files of a Unity project are interpeted as 'template files', which means that if the file contains the correct
structure of the below:

```C#
internal static class _Template_SoAndSo
{
    public static string Generate() => // ...
}
```

...the `Generate()` function will be called after compilation and its result will be pasted into a file of the same name
but with the 'result file' ending '`.tr.cs`'. After that Unity will detect the file change, and since both files end with
the C# file extention '`.cs`', Unity will reload and recompile its assets and the result will be interpreted as standard C#.
This is useful for writing the same performant code over and over for non-generic types in overloaded functions or similar
type definitions.

All of this would be unnecessary if Unity would just update to the latest .NET so I could have my
[generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math). Gawd!
