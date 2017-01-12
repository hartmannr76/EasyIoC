set -e

if [ -n "$TRAVIS_TAG" ]; then
   nuget setApiKey $NUGET_API_KEY

    # Pack the files for NuGet
    for proj in `ls */project.json`; do 
        if [[ "$proj" != *.Tests* ]]; then
            dotnet pack "$proj" -c Release -o ./.nupkg/;
        fi;
    done

    for pkg in `ls ./.nupkg/*.nupkg`; do
        # Push to NuGet
        nuget push "$pkg" -Verbosity detailed
    done
fi
