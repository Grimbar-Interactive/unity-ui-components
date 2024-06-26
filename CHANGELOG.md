# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

==
## [1.2.3] - 2024-04-02
### Updated
- Improving reusability and adding state-based canvas base classes.

## [1.2.2] - 2024-02-26
### Fixed
- Newer Unity Variables dependency.

## [1.2.1] - 2024-02-26
### Fixed
- Fixing dependencies.

## [1.2.0] - 2024-02-26
### Modified
- Converting from Unity-Components package to Unity-UIComponents

## [1.1.3] - 2024-02-24
### Modifed
- Updated package information

## [1.1.2] - 2024-02-13
### Modifed
- Assembly define constraints
- Organized files

## [1.1.1] - 2024-02-08
### Modified
- Made several UI components abstract
### Added
- QuitButton component
- OnClicked abstract method on ButtonComponent

## [1.1.0] - 2024-02-01
### Added
- Added support for Unity 2019.4.X versions.

## [1.0.9] - 2024-01-17
### Modified
- Making a generic MultiStateEnabledCanvas to allow extension.

## [1.0.8] - 2024-01-15
### Added
- AppVersionText

## [1.0.7] - 2023-11-16
### Modified
- BoolEnabledCanvas can now delay disabling.

## [1.0.6] - 2023-11-16
### Added
- MultiStateEnabledCanvas

## [1.0.5] - 2023-11-09
### Added
- StringVariableText
#### Modified
- Updated names of other text components (now IntVariableText and FloatVariableText)

## [1.0.4] - 2023-11-09
### Added
- Can now set delay states for StateEnabledCanvas

## [1.0.3] - 2023-11-01
### Fixed
- Making BoolEnabledCanvas enable/disable the canvas in OnEnable().

## [1.0.2] - 2023-11-01
### Added
- BoolEnabledCanvas
- ToggleComponent
### Modified
- Renamed StateCanvas to StateEnabledCanvas
- Made BoolVariableToggle inherit from ToggleComponent

## [1.0.1] - 2023-11-01
### Added
- BoolVariableToggle

## [1.0.0] - 2022-11-10
### Added
- Initial creation of Unity-Components.