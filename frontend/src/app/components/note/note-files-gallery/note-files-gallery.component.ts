import { Component, computed, input } from '@angular/core';
import { Note } from '../../../models/note';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { CloudinarySecurePipe } from '../../../pipes/cloudinary-secure.pipe';

@Component({
  selector: 'app-note-files-gallery',
  imports: [GalleryModule],
  templateUrl: './note-files-gallery.component.html',
  styleUrl: './note-files-gallery.component.css',
})
export class NoteFilesGalleryComponent {
  note = input.required<Note>();
  images = computed<GalleryItem[]>(() => {
    return this.note().noteFiles.map((note) => {
      return new ImageItem({ src: note.url, thumb: note.url });
    });
  });
}
