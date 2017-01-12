set -e

if [ -z "$TRAVIS_TAG" ]; then
   nuget setApiKey $NUGET_API_KEY

    # Pack the files for NuGet
    for proj in `ls */project.json`; do 
        if [[ "$proj" != *.Tests* ]]; then
            dotnet pack "$proj" -c Release -o ./.nupkg/;
        fi;
    done

    # Push to NuGet
    nuget push ./.nupkg/*.nupkg $NUGET_API_KEY -verbosity detailed
fi
