Option Explicit On
Option Strict On

Imports System.Runtime.InteropServices
Imports System.IO

Namespace Messaging

    ''' <summary>
    ''' リッチテキスト形式対応メッセージボックス
    ''' </summary>
    Public Class ExMessageBox

        ''' <summary>
        ''' リッチテキストファイルが格納されているフォルダパス
        ''' </summary>
        Public Shared Property FileDirectory As String

        ''' <summary>
        ''' パターンデータ
        ''' </summary>
        Private Shared _Patterns As List(Of TemplatePattern)

#Region "Overloads"

        ''' <summary>
        ''' 指定したテキスト、キャプション、ボタン、アイコン、既定のボタン、オプション、および [ヘルプ] ボタンを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <param name="options">メッセージ ボックスで使用する表示オプションと関連付けオプションを指定する System.Windows.Forms.MessageBoxOptions値の 1 つ。 既定値を使用する場合は、0 を渡します。</param>
        ''' <param name="displayHelpButton"> [ヘルプ] ボタンを表示する場合は true。それ以外の場合は false。 既定値は、false です。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, displayHelpButton As Boolean) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(text, caption, buttons, icon, defaultButton, options, displayHelpButton)
            End If
        End Function

        ''' <summary>
        ''' 指定したオブジェクトの前に、指定したテキスト、キャプション、およびボタンを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="owner">モーダル ダイアログ ボックスを所有する System.Windows.Forms.IWin32Window の実装。</param>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(owner As IWin32Window, text As String, caption As String, buttons As MessageBoxButtons) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(owner, text, caption, buttons)
            End If
        End Function

        ''' <summary>
        ''' 指定したオブジェクトの前に、指定したテキスト、キャプション、ボタン、およびアイコンを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="owner">モーダル ダイアログ ボックスを所有する <seealso cref="System.Windows.Forms.IWin32Window"/> の実装。</param>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(owner As IWin32Window, text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(owner, text, caption, buttons, icon)
            End If
        End Function

        ''' <summary>
        ''' 指定したオブジェクトの前に、指定したテキスト、キャプション、ボタン、アイコン、および既定のボタンを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="owner">モーダル ダイアログ ボックスを所有する System.Windows.Forms.IWin32Window の実装。</param>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(owner As IWin32Window, text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton)
            End If
        End Function

        ''' <summary>
        ''' 指定したオブジェクトの前に、指定したテキスト、キャプション、ボタン、アイコン、既定のボタン、およびオプションを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="owner">モーダル ダイアログ ボックスを所有する System.Windows.Forms.IWin32Window の実装。</param>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <param name="options">メッセージ ボックスで使用する表示オプションと関連付けオプションを指定する System.Windows.Forms.MessageBoxOptions値の 1 つ。 既定値を使用する場合は、0 を渡します。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(owner As IWin32Window, text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options)
            End If
        End Function


        ''' <summary>
        ''' 指定したテキストを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(text As String) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then
                Return ShowRichDialog(text, template)
            Else
                Return MessageBox.Show(text)
            End If
        End Function

        ''' <summary>
        ''' 指定したテキストとキャプションを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(text As String, caption As String) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(text, caption)
            End If
        End Function

        ''' <summary>
        ''' 指定したテキスト、キャプション、およびボタンを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(text, caption, buttons)
            End If
        End Function

        ''' <summary>
        ''' 指定したテキスト、キャプション、ボタン、およびアイコンを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(text, caption, buttons, icon)
            End If
        End Function

        ''' <summary>
        ''' 指定したオブジェクトの前に、指定したテキストとキャプションを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="owner">モーダル ダイアログ ボックスを所有する System.Windows.Forms.IWin32Window の実装。</param>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(owner As IWin32Window, text As String, caption As String) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(owner, text, caption)
            End If
        End Function

        ''' <summary>
        ''' 指定したテキスト、キャプション、ボタン、アイコン、および既定のボタンを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(text, caption, buttons, icon, defaultButton)
            End If
        End Function

        ''' <summary>
        ''' 指定したテキスト、キャプション、ボタン、アイコン、既定のボタン、オプション、および指定したヘルプ ファイル、HelpNavigator、およびヘルプ トピックを使用する [ヘルプ] ボタンを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="owner">モーダル ダイアログ ボックスを所有する System.Windows.Forms.IWin32Window の実装。</param>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <param name="options">メッセージ ボックスで使用する表示オプションと関連付けオプションを指定する System.Windows.Forms.MessageBoxOptions値の 1 つ。 既定値を使用する場合は、0 を渡します。</param>
        ''' <param name="helpFilePath">ユーザーが [ヘルプ] ボタンをクリックしたときに表示されるヘルプ ファイルのパスと名前。</param>
        ''' <param name="navigator"><seealso cref="System.Windows.Forms.HelpNavigator"/>値のいずれか 1 つ。</param>
        ''' <param name="param">ユーザーが [ヘルプ] ボタンをクリックしたときに表示されるヘルプ トピックの数値 ID。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(owner As IWin32Window, text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, helpFilePath As String, navigator As HelpNavigator, param As Object) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param)
            End If
        End Function


        ''' <summary>
        ''' 指定したテキスト、キャプション、ボタン、アイコン、既定のボタン、オプション、および指定したヘルプ ファイル、HelpNavigator、およびヘルプ トピックを使用する[ヘルプ] ボタンを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <param name="options">メッセージ ボックスで使用する表示オプションと関連付けオプションを指定する System.Windows.Forms.MessageBoxOptions値の 1 つ。 既定値を使用する場合は、0 を渡します。</param>
        ''' <param name="helpFilePath">ユーザーが [ヘルプ] ボタンをクリックしたときに表示されるヘルプ ファイルのパスと名前。</param>
        ''' <param name="navigator"><seealso cref="System.Windows.Forms.HelpNavigator"/>値のいずれか 1 つ。</param>
        ''' <param name="param">ユーザーが [ヘルプ] ボタンをクリックしたときに表示されるヘルプ トピックの数値 ID。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, helpFilePath As String, navigator As HelpNavigator, param As Object) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param)
            End If
        End Function

        ''' <summary>
        ''' 指定したテキスト、キャプション、ボタン、アイコン、既定のボタン、オプション、および指定したヘルプ ファイルと HelpNavigator を使用する [ヘルプ]ボタンを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="owner">モーダル ダイアログ ボックスを所有する System.Windows.Forms.IWin32Window の実装。</param>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <param name="options">メッセージ ボックスで使用する表示オプションと関連付けオプションを指定する System.Windows.Forms.MessageBoxOptions値の 1 つ。 既定値を使用する場合は、0 を渡します。</param>
        ''' <param name="helpFilePath">ユーザーが [ヘルプ] ボタンをクリックしたときに表示されるヘルプ ファイルのパスと名前。</param>
        ''' <param name="navigator"><seealso cref="System.Windows.Forms.HelpNavigator"/>値のいずれか 1 つ。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(owner As IWin32Window, text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, helpFilePath As String, navigator As HelpNavigator) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator)
            End If
        End Function

        ''' <summary>
        ''' 指定したテキスト、キャプション、ボタン、アイコン、既定のボタン、オプション、および指定したヘルプ ファイルと HelpNavigator を使用する [ヘルプ]ボタンを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <param name="options">メッセージ ボックスで使用する表示オプションと関連付けオプションを指定する System.Windows.Forms.MessageBoxOptions値の 1 つ。 既定値を使用する場合は、0 を渡します。</param>
        ''' <param name="helpFilePath">ユーザーが [ヘルプ] ボタンをクリックしたときに表示されるヘルプ ファイルのパスと名前。</param>
        ''' <param name="navigator"><seealso cref="System.Windows.Forms.HelpNavigator"/>値のいずれか 1 つ。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, helpFilePath As String, navigator As HelpNavigator) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator)
            End If
        End Function

        ''' <summary>
        ''' 指定したテキスト、キャプション、ボタン、アイコン、既定のボタン、オプション、および指定したヘルプ ファイルとヘルプ キーワードを使用する [ヘルプ] ボタンを表示するメッセージボックスを表示します。
        ''' </summary>
        ''' <param name="owner">モーダル ダイアログ ボックスを所有する System.Windows.Forms.IWin32Window の実装。</param>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <param name="options">メッセージ ボックスで使用する表示オプションと関連付けオプションを指定する System.Windows.Forms.MessageBoxOptions値の 1 つ。 既定値を使用する場合は、0 を渡します。</param>
        ''' <param name="helpFilePath">ユーザーが [ヘルプ] ボタンをクリックしたときに表示されるヘルプ ファイルのパスと名前。</param>
        ''' <param name="keyword">ユーザーが [ヘルプ] ボタンをクリックしたときに表示されるヘルプ キーワード。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(owner As IWin32Window, text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, helpFilePath As String, keyword As String) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword)
            End If
        End Function

        ''' <summary>
        ''' 指定したテキスト、キャプション、ボタン、アイコン、既定のボタン、オプション、および指定したヘルプ ファイルとヘルプ キーワードを使用する [ヘルプ] ボタンを表示するメッセージボックスを表示します。
        ''' </summary>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <param name="options">メッセージ ボックスで使用する表示オプションと関連付けオプションを指定する System.Windows.Forms.MessageBoxOptions値の 1 つ。 既定値を使用する場合は、0 を渡します。</param>
        ''' <param name="helpFilePath">ユーザーが [ヘルプ] ボタンをクリックしたときに表示されるヘルプ ファイルのパスと名前。</param>
        ''' <param name="keyword">ユーザーが [ヘルプ] ボタンをクリックしたときに表示されるヘルプ キーワード。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, helpFilePath As String, keyword As String) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword)
            End If
        End Function

        ''' <summary>
        ''' 指定したテキスト、キャプション、ボタン、アイコン、既定のボタン、オプション、および指定したヘルプ ファイルを使用する [ヘルプ] ボタンを表示するメッセージボックスを表示します。
        ''' </summary>
        ''' <param name="owner"></param>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <param name="options">メッセージ ボックスで使用する表示オプションと関連付けオプションを指定する System.Windows.Forms.MessageBoxOptions値の 1 つ。 既定値を使用する場合は、0 を渡します。</param>
        ''' <param name="helpFilePath">ユーザーが [ヘルプ] ボタンをクリックしたときに表示されるヘルプ ファイルのパスと名前。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(owner As IWin32Window, text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, helpFilePath As String) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options, helpFilePath)
            End If
        End Function

        ''' <summary>
        ''' 指定したテキスト、キャプション、ボタン、アイコン、既定のボタン、オプション、および指定したヘルプ ファイルを使用する [ヘルプ] ボタンを表示するメッセージボックスを表示します。
        ''' </summary>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <param name="options">メッセージ ボックスで使用する表示オプションと関連付けオプションを指定する System.Windows.Forms.MessageBoxOptions値の 1 つ。 既定値を使用する場合は、0 を渡します。</param>
        ''' <param name="helpFilePath">ユーザーが [ヘルプ] ボタンをクリックしたときに表示されるヘルプ ファイルのパスと名前。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, helpFilePath As String) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath)
            End If
        End Function

        ''' <summary>
        ''' 指定したテキスト、キャプション、ボタン、アイコン、既定のボタン、およびオプションを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <param name="caption">メッセージ ボックスのタイトル バーに表示するテキスト。</param>
        ''' <param name="buttons">メッセージ ボックスに表示するボタンを指定する System.Windows.Forms.MessageBoxButtons 値の 1 つ。</param>
        ''' <param name="icon">メッセージ ボックスに表示するアイコンを指定する System.Windows.Forms.MessageBoxIcon 値の 1 つ。</param>
        ''' <param name="defaultButton">メッセージ ボックスの既定のボタンを指定する System.Windows.Forms.MessageBoxDefaultButton 値の 1 つ。</param>
        ''' <param name="options">メッセージ ボックスで使用する表示オプションと関連付けオプションを指定する System.Windows.Forms.MessageBoxOptions値の 1 つ。 既定値を使用する場合は、0 を渡します。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(text, caption, buttons, icon, defaultButton, options)
            End If
        End Function


        ''' <summary>
        ''' 指定したオブジェクトの前に、指定したテキストを表示するメッセージ ボックスを表示します。
        ''' </summary>
        ''' <param name="owner">モーダル ダイアログ ボックスを所有する System.Windows.Forms.IWin32Window の実装。</param>
        ''' <param name="text">メッセージ ボックスに表示するテキスト。</param>
        ''' <returns>System.Windows.Forms.DialogResult 値のいずれか 1 つ。</returns>
        Public Shared Function Show(owner As IWin32Window, text As String) As DialogResult
            Dim template As TemplatePattern
            If IsRichTextMessage(text, template) Then

            Else
                Return MessageBox.Show(owner, text)
            End If
        End Function


#End Region

        ''' <summary>
        ''' 設定を読み込み、正規表現パターンを作る
        ''' </summary>
        Public Shared Sub Compile()
            _Patterns = New List(Of TemplatePattern)()

            ' 本来は設定を読み込む
            Dim patter As TemplatePattern
            patter = New TemplatePattern("(.+)(保存)(.+)", "sample.rtf", 700, 500)
            _Patterns.Add(patter)
        End Sub

        ''' <summary>
        ''' リッチテキスト形式ダイアログを表示する
        ''' </summary>
        ''' <param name="text">オリジナルのメッセージ</param>
        ''' <param name="template">テンプレート</param>
        ''' <returns>DialogResult</returns>
        Public Shared Function ShowRichDialog(ByVal text As String, ByRef template As TemplatePattern) As DialogResult
            Using frm As RichMessageDialog = New RichMessageDialog
                frm.Template = template
                frm.MessageText = text
                frm.ShowDialog()
                Return frm.DialogResult
            End Using
        End Function

        ''' <summary>
        ''' 正規表現にマッチするテンプレートを取得する
        ''' </summary>
        ''' <param name="text">オリジナルのメッセージ</param>
        ''' <param name="template">テンプレート</param>
        ''' <returns>True：テンプレート取得成功、False：取得失敗</returns>
        Private Shared Function IsRichTextMessage(ByVal text As String, <Out> ByRef template As TemplatePattern) As Boolean
            ' 表示対象のテキストに正規表現のパターンマッチしてヒットしたら採用
            For Each pattern As TemplatePattern In _Patterns
                If pattern.Pattern.IsMatch(text) AndAlso File.Exists(pattern.TemplatePath) Then
                    template = pattern
                    Return True
                End If
            Next
            Return False
        End Function

    End Class

End Namespace


