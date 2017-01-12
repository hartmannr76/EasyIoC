set -e

# Make sure all solutions build properly
for proj in `ls */project.json`; do
    dotnet build "$proj";
done

# Run any unit tests we have for these solutions
for proj in `ls *Tests*/project.json`; do
    dotnet test "$proj";
done
