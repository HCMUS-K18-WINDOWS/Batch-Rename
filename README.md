# Batch Rename

![alt text](https://i.imgur.com/uvWWhkj.png)

# Mục lục

- [Batch Rename](#batch-rename)
- [Mục lục](#mục-lục)
  - [Thông tin nhóm](#thông-tin-nhóm)
  - [Hướng dẫn chạy project](#hướng-dẫn-chạy-project)
  - [Bảng phân công](#bảng-phân-công)
  - [Kiến trúc và kỹ thuật](#kiến-trúc-và-kỹ-thuật)
  - [Danh sách chức năng đã hoàn thành.](#danh-sách-chức-năng-đã-hoàn-thành)
    - [Core requirements (5/5)](#core-requirements-55)
    - [Improvements (10/11)](#improvements-1011)
  - [Danh sách chức năng chưa hoàn thành](#danh-sách-chức-năng-chưa-hoàn-thành)
    - [Improvements](#improvements)
  - [Bonus](#bonus)
  - [Điểm tự đánh giá](#điểm-tự-đánh-giá)
  - [Github and video demo](#github-and-video-demo)

## Thông tin nhóm

| MSSV     | Họ và tên       |
| -------- | --------------- |
| 18120256 | Vũ Lê Tuấn      |
| 18120305 | Lê Quốc Đạt     |
| 18120467 | Huỳnh Quang Nam |

## Hướng dẫn chạy project

- Mở solution bằng Visual Studio 2022. (.NET 6.0)
- Solution gồm:
  - BatchRenameNew: project chính của chương trình.
  - RenameRuleContract: project chứa các interface hợp đồng.
  - RenameRuleLibrary và RuleLibrary2: lần lượt là các thư viện của chương trình gồm các luật đổi tên.
- Nhấn tổ hợp phím `Ctrl + Shift + B` để build project.
- Tiến hành copy cái RenameRuleLibrary.dll và RuleLibrary2.dll vào folder chứa file .exe của chương trình chính.

## Bảng phân công

| STT | Họ và tên       | Chức năng                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       | Đánh giá (h) |
| --- | --------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------ |
| 1   | Lê Quốc Đạt     | - Create a set of rules for renaming the files.<br> - Dynamically load all renaming rules from external DLL files. <br> - Storing parameters for renaming using XML file / JSON / excel / database. <br>- Save this set of rules into presets for quickly loading later if you need to reuse <br> - Last time state: When exiting the application, auto-save and load the <br> 1. The current size of the screen. <br>2. Current position of the screen. <br> 3. Last chosen preset. <br> - Autosave & load the current working condition to prevent sudden power loss <br> 1. The current file list <br> 2. The current set of renaming rules, together with the parameters <br> - Let the user see the preview of the result. | 35           |
| 2   | Huỳnh Quang Nam | Create a set of rules for renaming the files.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 3   | Vũ Lê Tuấn      | - Apply the set of rules in numerical order to each file, make them have a new name. <br> - Edit rule. <br> - Adding recursively: just specify a folder only, the application will automatically scan and add all the files inside. <br> - Handle duplication. <br> - Using regular expression. <br> - Checking exceptions when editing rules. <br> - Create a copy of all the files and move them to a selected folder rather than perform the renaming on the original file. <br> - Rename folders. <br> - Dynamically create UIElement for rules creation and editing.                                                                                                                                                       | 30           |

## Kiến trúc và kỹ thuật

- Design patterns: Singleton, Factory Method, Prototype.
- Plugin architecture.
- Delegate & event.
- C#, WPF.

## Danh sách chức năng đã hoàn thành.

### Core requirements (5/5)

1.  Dynamically load all renaming rules from external DLL files
2.  Select all files and folders you want to rename
3.  Create a set of rules for renaming the files.
    1.  Each rule can be added from a menu
    2.  Each rule's parameters can be edited
4.  Apply the set of rules in numerical order to each file, make them have a new name
5.  Save this set of rules into presets for quickly loading later if you need to reuse

### Improvements (10/11)

1.  Drag & Drop a file to add to the list.
2.  Storing parameters for renaming using XML file / JSON / excel / database
3.  Adding recursively: just specify a folder only, the application will automatically scan and add all the files inside
4.  Handling duplication
5.  Last time state: When exiting the application, auto-save and load the
    1.  The current size of the screen
    2.  Current position of the screen
    3.  Last chosen preset
6.  Autosave & load the current working condition to prevent sudden power loss
    1.  The current file list
    2.  The current set of renaming rules, together with the parameters
7.  Using regular expressions
8.  Checking exceptions when editing rules: like characters that cannot be in the file name, the maximum length of the filename cannot exceed 255 characters
9.  Let the user see the preview of the result.
10. Create a copy of all the files and move them to a selected folder rather than perform the renaming on the original file.

## Danh sách chức năng chưa hoàn thành

### Improvements

9.  Save and load your work into a project.

## Bonus

- Thiết kế UI tùy biến theo kích thước.
- Validate giá trị của rule parameter khi edit và create rule.

## Điểm tự đánh giá

10/10

## Github and video demo

- Github: https://github.com/lequocdatfit/Batch-Rename
- Video demo:
