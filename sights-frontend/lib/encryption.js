    const generateKey = async () => {
        return window.crypto.subtle.generateKey({
            name:'AES-GCM',
            length: '256',
        },
        true,
        ['encrypt', 'decrypt'])
    }

    const encode = (data) => {
        const encoder = new TextEncoder()
        return encoder.encode(data)
    }

    const generateIv = () => {
        return window.crypto.getRandomValues(new Uint8Array(12))
    }

    const encrypt = async (data, key) => {
        const encoded = encode(data)
        const iv = generateIv();
        const cipher = await window.crypto.subtle.encrypt({
            name:'AES-GCM',
            iv: iv,
        }, key, encoded)

        return {
            cipher,
            iv,
        }
    }

    const pack = (buffer) => {
        return window.btoa(
            String.fromCharCode.apply(null, new Uint8Array(buffer))
        )
    }

    const unpack = (packed) => {
        const text = window.atob(packed)
        const buffer = new ArrayBuffer(text.length)
        const bufferView = new Uint8Array(buffer)

        for (let i = 0; i < string.length; i++){
            bufferView[i] = text.charCodeAt(i);
        }

        return buffer
    }

    const decode = (bytestream) => {
        const decoder = new TextDecoder()

        return decoder.decode(bytestream);
    }

    const decrypt = async (cipher, key, iv) => {
        const encoded = await window.crypto.subtle.decrypt({
            name: 'AES-GCM',
            iv: iv,
        }, key, cipher)
        return decode(encoded)
        }

      export async function getSafe (url) {
  // encrypt message
  const first = 'Hello, World!'
  const key = await generateKey()
  const { cipher, iv } = await encrypt(first, key)
  // pack and transmit
  await fetch(url, {
    method: 'POST',
    body: JSON.stringify({
      cipher: pack(cipher),
      iv: pack(iv),
    }),
  })
  // retrieve
  const response = await fetch(url).then(res => res.json())
  // unpack and decrypt message
  const final = await decrypt(unpack(response.cipher), key, unpack(response.iv))
  console.log(final) // logs 'Hello, World!'
}