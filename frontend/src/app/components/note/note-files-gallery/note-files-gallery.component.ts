import { Component, computed, input, output } from '@angular/core';
import { Note } from '../../../models/note';
import {
  GalleryItem,
  GalleryModule,
  GalleryState,
  ImageItem,
  VideoItem,
} from 'ng-gallery';
import { NoteFile } from '../../../models/noteFile';

@Component({
  selector: 'app-note-files-gallery',
  imports: [GalleryModule],
  templateUrl: './note-files-gallery.component.html',
  styleUrl: './note-files-gallery.component.css',
})
export class NoteFilesGalleryComponent {
  note = input.required<Note>();
  id = input('gallery');
  extension = '';
  class = input('');
  minWidth = input<string>('');
  onIndexChange = output<NoteFile>();
  images = computed<GalleryItem[]>(() => {
    return this.note().noteFiles.map((note) => {
      const extension = note.url.split('.').pop()?.toLocaleLowerCase();
      this.extension = extension || '';
      const isVideo = ['mp4', 'webm', 'ogg', 'mov'].includes(extension || '');

      if (isVideo) {
        return new VideoItem({
          src: note.url,
          thumb: note.url,
          poster: note.url,
          type: extension || `video/${extension}`, // Default MIME type if not specified
        });
      }

      if (extension === 'pdf') {
        const src = `/images/pdf-logo.png`;
        return new ImageItem({ src, thumb: src });
      }

      if (extension === 'txt') {
        const src = `/images/txt-logo.png`;
        return new ImageItem({ src, thumb: src });
      }

      return new ImageItem({ src: note.url, thumb: note.url });
    });
  });

  handleIndexChange(state: GalleryState) {
    const index = state.currIndex;
    if (!index) return;

    const image = this.note().noteFiles[index];
    if (image) this.onIndexChange.emit(image);
  }
}
