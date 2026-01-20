# Release Process Documentation

This document describes the automated release process and how to manually control versioning.

## Automated Release Workflow

The release workflow is automatically triggered when:
1. Changes are pushed to the `main` branch
2. Manually triggered via the GitHub Actions UI

### What the Workflow Does

1. **Builds the application** using .NET 10
2. **Generates version numbers** using Nerdbank.GitVersioning (nbgv)
3. **Publishes the application** for distribution
4. **Creates a GitHub Release** with the version-tagged archive

### Version Numbering

The project uses [Nerdbank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning) for automatic semantic versioning.

- **Current version**: Defined in `version.json`
- **Patch version** (last number): Auto-increments with each commit on `main` branch
- **Version format**: `MAJOR.MINOR.PATCH` (e.g., `1.0.5`)

## How to Increment Version Numbers Manually

### Incrementing the Patch Version (1.0.X)

The patch version automatically increments with each commit merged to `main`. No manual action needed.

### Incrementing the Minor Version (1.X.0)

To increment the minor version (e.g., from `1.0.x` to `1.1.0`):

1. Install the nbgv tool (if not already installed):
   ```bash
   dotnet tool install --global nbgv
   ```

2. Run the following command from the repository root:
   ```bash
   nbgv set-version 1.1
   ```

3. Commit and push the changes to `version.json`:
   ```bash
   git add version.json
   git commit -m "Bump version to 1.1"
   git push
   ```

### Incrementing the Major Version (X.0.0)

To increment the major version (e.g., from `1.x.x` to `2.0.0`):

1. Install the nbgv tool (if not already installed):
   ```bash
   dotnet tool install --global nbgv
   ```

2. Run the following command from the repository root:
   ```bash
   nbgv set-version 2.0
   ```

3. Commit and push the changes to `version.json`:
   ```bash
   git add version.json
   git commit -m "Bump version to 2.0"
   git push
   ```

## Best Practices

1. **Patch versions** (bug fixes, small changes): Let them auto-increment
2. **Minor versions** (new features, backward-compatible): Increment manually before merging feature branches
3. **Major versions** (breaking changes): Increment manually in a dedicated PR

## Manual Release Trigger

To manually trigger a release without pushing to `main`:

1. Go to the repository on GitHub
2. Navigate to **Actions** → **Release**
3. Click **Run workflow**
4. Select the branch (usually `main`)
5. Click **Run workflow**

## Versioning in Pull Requests

When you want to increment the major or minor version as part of a pull request:

1. Create your feature branch
2. Make your changes
3. Before creating the PR, run `nbgv set-version X.Y` to set the new version
4. Commit the `version.json` change along with your other changes
5. Create the pull request
6. When merged to `main`, the release will use the new version number

## Release Artifacts

Each release includes:
- **Tag**: `vX.Y.Z` (e.g., `v1.0.5`)
- **Release archive**: `CopyHelper-X.Y.Z.zip` containing the published application
- **Release notes**: Auto-generated with version information and installation instructions

## Troubleshooting

### Release workflow fails

- Check the Actions tab for detailed error logs
- Ensure the `version.json` file is valid JSON
- Verify that .NET 10 SDK is available

### Version number is unexpected

- Check the `version.json` file for the current base version
- Run `nbgv get-version` locally to see what version would be generated
- Remember that the build number is based on commit height since the last version change

## Required Permissions

The release workflow requires the following GitHub permissions:
- **contents: write** - To create releases and tags

These permissions are configured in the workflow file and should work automatically in the repository.
