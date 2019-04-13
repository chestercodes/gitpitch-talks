ls -R  | where { ! $_.PSIsContainer } | where { ! $_.Directory.Name.ToString().StartsWith(".") } | where { ! $_.Name.ToString().StartsWith(".") } | Select-object FullName

ls -R | 
    where { ! $_.PSIsContainer } | 
    where { ! $_.Directory.Name.ToString().StartsWith(".") } | 
    where { ! $_.Name.ToString().StartsWith(".") } | 
    Select-object FullName

ls -R | 
    where { ! $_.PSIsContainer } | 
    where { ! $_.Directory.Name.ToString().StartsWith(".") } | 
    where { ! $_.Name.ToString().StartsWith(".") } | 
    where { ! $_.Name.ToString().EndsWith(".png") } | 
    where { ! $_.Name.ToString().EndsWith("xml") } | 
    Select-object FullName