import React, { useState } from 'react';
import { createRoom, updateRoom } from '../../services/roomService';

const RoomForm = ({ room, onSuccess }) => {
    const [name, setName] = useState(room ? room.name : '');
    const [floor, setFloor] = useState(room ? room.floor : '');

    const handleSubmit = async (e) => {
        e.preventDefault();
        const roomData = { name, floor };
        if (room) {
            await updateRoom(room.id, roomData);
        } else {
            await createRoom(roomData);
        }
        onSuccess();
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                value={name}
                onChange={(e) => setName(e.target.value)}
                placeholder="Room Name"
                required
            />
            <input
                type="number"
                value={floor}
                onChange={(e) => setFloor(e.target.value)}
                placeholder="Floor"
                required
            />
            <button type="submit">{room ? 'Update' : 'Create'} Room</button>
        </form>
    );
};

export default RoomForm;