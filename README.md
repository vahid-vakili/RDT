
# RDT

![GitHub release](https://img.shields.io/github/v/release/vahid-vakili/RDT)
![License](https://img.shields.io/github/license/vahid-vakili/RDT)
![Issues](https://img.shields.io/github/issues/vahid-vakili/RDT)


RDT is designed to manage the login process to Windows and Linux hosts.

Since Remote Desktop (Windows) and SSH access are among the most frequently used operations in both test and production environments, maintaining or remembering usernames and passwords for each host can be difficult. Additionally, hosts may belong to different domains. This tool has been implemented to centralize and simplify the login process while managing credentials in an organized manner.

The tool is simple to use and, depending on its configuration, provides a wide range of useful capabilities.

## Overview

In general, the tool consists of a TreeView and two main tabs: **Home** and **Credentials**.

- **TreeView**  
  The TreeView is populated based on the hosts defined in the configuration file.

- **Home**  
  This tab is used to perform Remote Desktop or SSH operations according to the hosts selected in the TreeView on the left side of the application.

- **Credentials**  
  This tab is used to store usernames and passwords required for logging into the target hosts.  
  **Note:** If a username has already been saved in Windows for Remote Desktop, it must be removed first.

## Key Features and Notes

- The tool does not require installation and can be placed in a shared network path. Shortcuts can then be created on the desktops of multiple users.
- Before using the **Home** tab, the relevant hosts must be selected from the TreeView and their credentials must be stored in the **Credentials** tab.
- All operations are performed only on the hosts that are selected in the TreeView (for both Home and Credentials tabs).
- Hostnames or IP addresses must be separated by semicolons (`;`).
- An unlimited number of categories can be defined in the configuration file, with hostnames or IP addresses specified in the `ENV` tag.
- To use SSH functionality for Linux hosts, the `putty.exe` file must be available in the application directory.
- If a username or password for a host changes, it must be removed or updated via the **Credentials** tab to prevent account lockout.

## Background

The initial version of this tool was released in 2020. Despite the passage of several years and the availability of more advanced similar tools, RDT has continued to remain practical and effective.


## Status
This project is actively maintained and open for contributions.
