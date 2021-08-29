export class InputUtils {
  public static insertAtCaret(input: HTMLTextAreaElement | HTMLInputElement, text: string) {

    // @ts-ignore
    if (document.selection) {
      input.focus();
      // @ts-ignore
      const sel = document.selection.createRange();
      sel.text = text;
    } else if (input.selectionStart || input.selectionStart === 0) {
      // Others
      const startPos = input.selectionStart;
      const endPos = input.selectionEnd;
      input.value = input.value.substring(0, startPos) + text +
        input.value.substring(endPos ?? 0, input.value.length);
      input.selectionStart = startPos + text.length;
      input.selectionEnd = startPos + text.length;
    } else {
      input.value += text;
    }
  }
}
